$(document).ready(function() {
    let currentPage = 1;
    const pageSize = 6;
    
    // 初始化页面
    initializePage();
    
    function initializePage() {
        loadFilterOptions();
        searchProperties();
    }
    
    // 加载过滤选项
    function loadFilterOptions() {
        $.get('/Home/GetFilterOptions')
            .done(function(data) {
                populateSelect('#regionSelect', data.regions);
                populateSelect('#districtSelect', data.districts);
                populateSelect('#propertyTypeSelect', data.propertyTypes);
            })
            .fail(function() {
                console.error('Failed to load filter options');
            });
    }
    
    // Populate dropdown
    function populateSelect(selector, options) {
        const $select = $(selector);
        const defaultOption = $select.find('option:first');
        $select.empty().append(defaultOption);
        
        options.forEach(function(option) {
            $select.append(`<option value="${option}">${option}</option>`);
        });
    }
    
    // Search button click event
    $('#searchBtn').click(function() {
        currentPage = 1;
        searchProperties();
    });
    
    // Reset button click event
    $('#resetBtn').click(function() {
        $('#searchForm')[0].reset();
        currentPage = 1;
        searchProperties();
    });
    
    // Search properties
    function searchProperties() {
        const filter = getSearchFilter();
        
        showLoading(true);
        
        $.ajax({
            url: '/Home/SearchProperties',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(filter),
            success: function(data) {
                displayProperties(data.items);
                displayPagination(data);
                showLoading(false);
            },
            error: function() {
                console.error('Search failed');
                showLoading(false);
                displayError('Search failed, please try again later');
            }
        });
    }
    
    // Get search filter criteria
    function getSearchFilter() {
        const priceRange = $('#priceRange').val();
        let minPrice = null;
        let maxPrice = null;
        
        if (priceRange) {
            const prices = priceRange.split('-');
            minPrice = parseInt(prices[0]);
            maxPrice = parseInt(prices[1]);
        }
        
        return {
            region: $('#regionSelect').val() || null,
            district: $('#districtSelect').val() || null,
            propertyType: $('#propertyTypeSelect').val() || null,
            minPrice: minPrice,
            maxPrice: maxPrice,
            pageNumber: currentPage,
            pageSize: pageSize
        };
    }
    
    // Display property list
    function displayProperties(properties) {
        const $container = $('#propertyResults');
        
        if (!properties || properties.length === 0) {
            $container.html(`
                <div class="empty-state">
                    <div class="mb-3">🏠</div>
                    <h5>No properties found</h5>
                    <p>Please try adjusting your search criteria</p>
                </div>
            `);
            return;
        }
        
        let html = '<div class="row">';
        
        properties.forEach(function(property) {
            html += createPropertyCard(property);
        });
        
        html += '</div>';
        $container.html(html);
    }
    
    // Create property card
    function createPropertyCard(property) {
        const formattedPrice = formatPrice(property.salePrice);
        const imageUrl = property.imageUrl || '/images/default-property.jpg';
        
        return `
            <div class="col-lg-4 col-md-6 col-sm-12">
                <div class="property-card">
                    <div class="property-image" style="background-image: url('${imageUrl}'); background-size: cover; background-position: center;">
                        ${!property.imageUrl ? '<span>No Image</span>' : ''}
                    </div>
                    <div class="property-info">
                        <div class="property-title">${property.title}</div>
                        <div class="property-price">${formattedPrice}</div>
                        <div class="property-details">
                            <div>${property.region} · ${property.district}</div>
                            <div>${property.propertyType} · ${property.area}㎡</div>
                        </div>
                        <div class="property-location">${property.address}</div>
                        <div class="property-meta">
                            <div class="property-specs">
                                ${property.bedrooms > 0 ? `<span class="property-spec">🛏️ ${property.bedrooms} bed</span>` : ''}
                                ${property.bathrooms > 0 ? `<span class="property-spec">🚿 ${property.bathrooms} bath</span>` : ''}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        `;
    }
    
    // Format price
    function formatPrice(price) {
        if (price >= 1000000) {
            return `$${(price / 1000000).toFixed(1)}M`;
        } else if (price >= 1000) {
            return `$${(price / 1000).toFixed(0)}K`;
        } else {
            return `$${price.toLocaleString()}`;
        }
    }
    
    // Display pagination
    function displayPagination(data) {
        const $nav = $('#paginationNav');
        const $pagination = $('#pagination');
        
        if (data.totalPages <= 1) {
            $nav.hide();
            return;
        }
        
        $nav.show();
        $pagination.empty();
        
        // Previous page
        if (data.hasPreviousPage) {
            $pagination.append(`
                <li class="page-item">
                    <a class="page-link" href="#" data-page="${data.pageNumber - 1}">Previous</a>
                </li>
            `);
        }
        
        // Page numbers
        const startPage = Math.max(1, data.pageNumber - 2);
        const endPage = Math.min(data.totalPages, data.pageNumber + 2);
        
        for (let i = startPage; i <= endPage; i++) {
            const isActive = i === data.pageNumber ? 'active' : '';
            $pagination.append(`
                <li class="page-item ${isActive}">
                    <a class="page-link" href="#" data-page="${i}">${i}</a>
                </li>
            `);
        }
        
        // Next page
        if (data.hasNextPage) {
            $pagination.append(`
                <li class="page-item">
                    <a class="page-link" href="#" data-page="${data.pageNumber + 1}">Next</a>
                </li>
            `);
        }
        
        // Pagination click event
        $pagination.find('a').click(function(e) {
            e.preventDefault();
            const page = parseInt($(this).data('page'));
            if (page && page !== currentPage) {
                currentPage = page;
                searchProperties();
                $('html, body').animate({ scrollTop: 0 }, 300);
            }
        });
    }
    
    // Show/hide loading indicator
    function showLoading(show) {
        if (show) {
            $('#loadingIndicator').show();
            $('#propertyResults').hide();
            $('#paginationNav').hide();
        } else {
            $('#loadingIndicator').hide();
            $('#propertyResults').show();
        }
    }
    
    // Display error message
    function displayError(message) {
        $('#propertyResults').html(`
            <div class="alert alert-danger" role="alert">
                <strong>Error:</strong> ${message}
            </div>
        `).show();
    }
});
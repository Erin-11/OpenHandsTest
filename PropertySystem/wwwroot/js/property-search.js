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
                console.error('加载过滤选项失败');
            });
    }
    
    // 填充下拉框
    function populateSelect(selector, options) {
        const $select = $(selector);
        const defaultOption = $select.find('option:first');
        $select.empty().append(defaultOption);
        
        options.forEach(function(option) {
            $select.append(`<option value="${option}">${option}</option>`);
        });
    }
    
    // 搜索按钮点击事件
    $('#searchBtn').click(function() {
        currentPage = 1;
        searchProperties();
    });
    
    // 重置按钮点击事件
    $('#resetBtn').click(function() {
        $('#searchForm')[0].reset();
        currentPage = 1;
        searchProperties();
    });
    
    // 搜索房产
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
                console.error('搜索失败');
                showLoading(false);
                displayError('搜索失败，请稍后重试');
            }
        });
    }
    
    // 获取搜索过滤条件
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
    
    // 显示房产列表
    function displayProperties(properties) {
        const $container = $('#propertyResults');
        
        if (!properties || properties.length === 0) {
            $container.html(`
                <div class="empty-state">
                    <div class="mb-3">🏠</div>
                    <h5>暂无房产信息</h5>
                    <p>请尝试调整搜索条件</p>
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
    
    // 创建房产卡片
    function createPropertyCard(property) {
        const formattedPrice = formatPrice(property.salePrice);
        const imageUrl = property.imageUrl || '/images/default-property.jpg';
        
        return `
            <div class="col-lg-4 col-md-6 col-sm-12">
                <div class="property-card">
                    <div class="property-image" style="background-image: url('${imageUrl}'); background-size: cover; background-position: center;">
                        ${!property.imageUrl ? '<span>暂无图片</span>' : ''}
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
                                ${property.bedrooms > 0 ? `<span class="property-spec">🛏️ ${property.bedrooms}室</span>` : ''}
                                ${property.bathrooms > 0 ? `<span class="property-spec">🚿 ${property.bathrooms}卫</span>` : ''}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        `;
    }
    
    // 格式化价格
    function formatPrice(price) {
        if (price >= 10000) {
            return `¥${(price / 10000).toFixed(0)}万`;
        } else {
            return `¥${price.toLocaleString()}`;
        }
    }
    
    // 显示分页
    function displayPagination(data) {
        const $nav = $('#paginationNav');
        const $pagination = $('#pagination');
        
        if (data.totalPages <= 1) {
            $nav.hide();
            return;
        }
        
        $nav.show();
        $pagination.empty();
        
        // 上一页
        if (data.hasPreviousPage) {
            $pagination.append(`
                <li class="page-item">
                    <a class="page-link" href="#" data-page="${data.pageNumber - 1}">上一页</a>
                </li>
            `);
        }
        
        // 页码
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
        
        // 下一页
        if (data.hasNextPage) {
            $pagination.append(`
                <li class="page-item">
                    <a class="page-link" href="#" data-page="${data.pageNumber + 1}">下一页</a>
                </li>
            `);
        }
        
        // 分页点击事件
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
    
    // 显示/隐藏加载指示器
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
    
    // 显示错误信息
    function displayError(message) {
        $('#propertyResults').html(`
            <div class="alert alert-danger" role="alert">
                <strong>错误：</strong> ${message}
            </div>
        `).show();
    }
});
var homeconfig = {
    pageSize: 12,
    pageIndex: 1

};

function loadData() {
    var name = '';
    var status = '';  
    name = $('input#productName').val();


    $.ajax({
        url: '/Product/GetJsonData',
        type: 'GET',
        data: {
            productName: name,
            page: homeconfig.pageIndex,
            pageSize: homeconfig.pageSize
        },
        dataType: 'json',
        success: function (response) {
            if (response.ReturnStatus) {
                // Grab the template script
                var theTemplateScript = $("#example-template").html();
                // var url = '/Asset/Client/Templates/About.html';

                // Compile the template
                var theTemplate = Handlebars.compile(theTemplateScript);

                // This is the default context, which is passed to the template
                var context = {
                    products: response.ProductDTOs
                };
                // Pass our data to the template
                var theCompiledHtml = theTemplate(context);

                // Add the compiled html to the page
                // $(document.body).append(theCompiledHtml);
                $('#productsGrid').empty();
                $('#productsGrid').append(theCompiledHtml);

                var changePageSize = false;

                paging(response.total, function () {
                    loadData();
                }, changePageSize);
            }
        }
    });
}


function paging(totalRow, callback, changePageSize, isAjax) {
    var totalPage = Math.ceil(totalRow / homeconfig.pageSize);

    //Unbind pagination if it existed or click change pagesize
    if ($('#pagination a').length === 0 || changePageSize === true) {
        $('#pagination').empty();
        $('#pagination').removeData("twbs-pagination");
        $('#pagination').unbind("page");
    }

    $('#pagination').twbsPagination({
        totalPages: totalPage,
        first: "First",
        next: "Next",
        last: "Last",
        prev: "Prev",
        visiblePages: 4,
        onPageClick: function (event, page) {
            homeconfig.pageIndex = page;
            $('#pageOnTotal').html('Page: ' + page + ' / ' + totalPage);
            
            if (page !== 1) {
                isAjax = true;
            }
            if (isAjax) {
                topFunction();
                setTimeout(callback, 200);
            }

        }
    });
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    document.body.scrollTop = 0; // For Safari
    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
} 

$(function () {
    // loadData();

    var isAjax = false;

    var totalRowsHtml = $('#p-totalRows').html();
    // p - productName
    var productName = $('#p-productName').html();

    $('input#productName').val(productName);

    var totalRows = Number(totalRowsHtml);
   
    paging(totalRows, function () {
        loadData();
    }, false, isAjax);
});
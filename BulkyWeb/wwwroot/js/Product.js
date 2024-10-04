$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    let table = $('#myTable').DataTable({
        "ajax": {
            "url": '/admin/product/getall', // Corrected the syntax
            "type": "GET" // You can specify the request type, default is GET
        },
        "columns": [
            { data: 'title', "width": "15%" },
            { data: 'isbn', "width": "15%" },
            { data: 'author', "width": "15%" },
            { data: 'listPrice', "width": "15%" },
            { data: 'price', "width": "15%" },
            { data: 'price50', "width": "15%" },
            { data: 'price100', "width": "15%" },
            { data: 'category.name', "width": "15%" },


        ]
    });
}

var ProductController = function () {
    this.callAddItem = function () {
        action();
    }
    function action() {
        $('body').on('click', '.btn-plus', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            const quantity = parseInt($('#txt_quantity_' + id).val()) + 1;
            Additem(id, quantity);
        });
    }
    function Additem(id, quantity) {
        const culture = $('#hidCulture').val();
        $.ajax({
            type: "POST",
            url: "/" + culture + '/Cart/UpdateCart',
            data: {
                id: id,
                quantity: quantity
            },
            success: function (res) {
                $('#lbl_number_items_header').text(res.length);
                loadData();
            },
            error: function (err) {
                console.log(err);
            }
        });
    }

    function Add(){
        const id = $('#id').val();
        console.log("id  ==== ",id);
    }
    
    this.themItem = function () {
        Add();
    }
    
}
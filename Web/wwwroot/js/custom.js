$(document).ready(function () {


    $(document).on('click', '#question', function () {

        var id = $(this).data('id');
        console.log("sagol")
        $.ajax({
            method: "GET",
            url: "/faq/LoadQuestions",
            data: {
                id: id
            },
            success: function (result) {
                $('.accordion').empty();
                $('.accordion').append(result);
            },
            error: function (e) {
                console.log(e)
            }
        })

    })
})
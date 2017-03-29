$.validator.addMethod('date', function (value, element) {
    var d = new Date();
    return this.optional(element) || !/Invalid|NaN/.test(new Date(d.toLocaleDateString(value)));
});

//@*<script>
//    $.validator.addMethod("anyDate",
//            function (value, element) {
//                return value.match(/^(0?[1-9]|[12][0-9]|3[0-1])[/., -](0?[1-9]|1[0-2])[/., -](19|20)?\d{2}$/);
//            },
//            "Please enter a date in the format!"
//        );
//        $('#form0')
//            .validate({
//        rules: {
//        field: {
//        anyDate: true
//                    }
//                }
//            })
//    </script> *@

//jQuery(function ($) {
//    $.validator.addMethod('date',
//    function (value, element) {
//        if (this.optional(element)) {
//            return true;
//        }
//        var ok = true;
//        try {
//            $.datepicker.parseDate('dd-mm-yyyy', value);
//        }
//        catch (err) {
//            ok = false;
//        }
//        return ok;
//    });
//});
var SubmitSaveContinue = function (obj) {
    var form = obj.form,
        btn = obj.btn;

    if (btn.is("[name='save-continue']")) {
        if (form.find("input:hidden[name='save-continue']").length == 0) {
            $("<input type='hidden' name='save-continue' value='1' />").appendTo(form);
        }
    }
    else {
        form.remove("input:hidden[name='save-continue']");
    }
    form.submit();
};
// wrap form with validation class
function wrapForm() {
    if ($('.wb-frmvld').length === 0) {
        // wrap all the form with the class for the validation
        $('form').wrap('<div class="wb-frmvld"></div>');
    }
}

// validate the form
function onEachRequest(sender, args) {
    var element = args.get_postBackElement();
    if ((element.type === 'submit' || element.hasAttribute('formsubmit')) && !element.hasAttribute('formnovalidate')) {
        args.set_cancel(!$('form').valid());
    }
}

// postback validation
function postbackValidation() {
    var validator = $('form').data('validator');
    $.each(validator.invalid, function (index, value) {
        if (value) {
            var input = $('#' + index.replace(/\$/g, '_'));
            if (input.length) input.valid();
        }
    });
    checkErrors();
}

// checkboxlist event
function fixCheckBoxList() {
    $(':checkbox[data-rule-require_from_group]').closest('fieldset').find('div').on('change', function () {
        $(this).closest('fieldset').find(':checkbox[data-rule-require_from_group]').valid();
    });
}

// init datepicker
function initDatePicker() {
    $('input[type=date]').trigger('wb-init.wb-date');
}

// check errors
function checkErrors() {
    var formId = $('form').attr('id');
    var errorFormId = '#errors-' + (!formId ? 'default' : formId);
    var errors = $(errorFormId);

    if (errors.length) {
        var id, input;
        errors.find('a').each(function () {
            id = $(this).attr('href');
            input = $(id);
            if (input.length == 0) $(this).remove();
        });
        if (errors.find('a').length == 0) errors.hide();
    }
}
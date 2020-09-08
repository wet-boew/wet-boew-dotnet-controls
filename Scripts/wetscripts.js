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

// fixRadioCheckbox event
function fixRadioCheckbox() {
    $('div.checkbox, div.checkbox-inline, div.radio, div.radio-inline').children('label').css("font-weight", "inherit").prepend(function () {
        return $(this).siblings('input');
    });
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
            if (input.length === 0) $(this).remove();
        });
        if (errors.find('a').length === 0) errors.hide();
    }
}

// show modal
function showModal(id, isClosable) {
    wb.doc.trigger('open.wb-lbx', [
        [
            { src: '#' + id, type: 'inline' }
        ], !isClosable
    ]);
}

// government validator method
function frmvldGovemail(errorMsg) {
    if ($.validator && $.validator !== 'undefined') {
        // create function for a specific validation method
        (function () {
            // the actual validation method govemail is the key to call the function
            $.validator.addMethod('govemail', function (value, element, params) {
                return this.optional(element) || /^[a-zA-Z0-9.!#$%&'*+\\/=?^_`{|}~-]+@((?:[a-zA-Z]([a-zA-Z0-9-.]{0,61}[a-zA-Z0-9]).gc{1})|canada|scc-csc)(.ca){1}$/.test(value);
            }, $.validator.format(errorMsg)); // this is the default string
        }());
    }
}


// price validator method
function frmvldPrice(errorMsg) {
    if ($.validator && $.validator !== 'undefined') {
        // create function for a specific validation method
        (function () {
            // the actual validation method price is the key to call the function
            jQuery.validator.addMethod('price', function (value, element, params) {
                return this.optional(element) || /^[0-9]+([\,|\.]{0,1}[0-9]{2}){0,1}$/.test(value);
            }, jQuery.validator.format(errorMsg)); // this is the default string
        }());
    }
}

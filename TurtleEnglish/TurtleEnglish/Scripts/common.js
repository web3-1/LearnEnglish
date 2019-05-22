//show dropdown menu:
function showDropDown() {
    $('.learn_menu').css('display', 'block');
}
function hiddenDropDown() {
    if ($('.learn_menu:hover').length === 0) {
        // do something ;)
        $('.learn_menu').css('display', 'none');
    }
}
//
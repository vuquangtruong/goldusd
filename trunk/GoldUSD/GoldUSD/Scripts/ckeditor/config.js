/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    //config.toolbarGroups = [
    //{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
    ////{ name: 'styles'},
    //{ name: 'colors' },
    //{ name: 'paragraph', groups: ['list', 'indent', 'align'] }
    //];
    config.toolbar = [
	{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
    { name: 'styles', items: ['Font', 'FontSize'] },
    { name: 'colors', items: ['TextColor', 'BGColor'] },
	{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-'] }
    ];
    config.removePlugins = 'elementspath';
    config.enterMode = CKEDITOR.ENTER_BR;
    config.height = '300px';
};
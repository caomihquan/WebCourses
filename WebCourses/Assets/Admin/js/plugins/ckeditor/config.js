/**
 * @license Copyright (c) 2003-2021, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
	config.syntaxhighlight_lang = 'csharp';
	config.syntaxhighlight_hideControls = true;
	config.language = 'vi';
	config.filebrowserBrowseUrl = '/Assets/Admin/js/plugins/ckfinder/ckfinder.html';
	config.filebrowserImageBrowseUrl = '/Assets/Admin/js/plugins/ckfinder/ckfinder.html?Type=Images';
	config.filebrowserFlashBrowseUrl = '/Assets/Admin/js/plugins/ckfinder/ckfinder.html?Type=Flash';
	config.filebrowserUploadUrl = '/Assets/Admin/js/plugins/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Files';
	config.filebrowserImageUploadUrl = '/Assets/Admin/js/plugins/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Images';
	config.filebrowserImageBrowseUrl = '/Data';
	config.filebrowserFlashBrowseUrl = '/Assets/Admin/js/plugins/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Flash';
	config.filebrowserWindowWidth='1000';
	config.filebrowserWindowHeight = '700';

	CKFinder.setupCKEditor(null, '/Assets/Admin/js/plugins/ckfinder/');
};

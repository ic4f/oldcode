tinyMCE.init({
    mode: "specific_textareas",
    editor_selector: "mceEditor",
    theme: "advanced",
    plugins: "style,layer,table,save,advhr,advimage,advlink,emotions,iespell,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras",
    button_tile_map: true,
    theme_advanced_buttons1: "bold,italic,underline,separator,justifyleft,justifycenter,justifyright,justifyfull,separator,bullist,numlist,outdent,indent,separator,forecolor,backcolor,separator,sub,sup,separator,code,preview,fullscreen,help",
    theme_advanced_buttons2: "cut,copy,pastetext,pasteword,separator,undo,redo,separator,search,replace,separator,link,unlink,anchor,image,advhr,charmap,emotions,media,separator,insertdate,inserttime",
    theme_advanced_buttons3: "formatselect,fontsizeselect,separator,nonbreaking,visualchars,separator,visualaid,removeformat,cleanup,separator",
    theme_advanced_buttons4: "tablecontrols,separator,styleprops,separator,cite,abbr,acronym,del,ins",


    theme_advanced_toolbar_location: "top",
    theme_advanced_toolbar_align: "left",
    theme_advanced_path_location: "bottom",
    theme_advanced_resizing: true,
    theme_advanced_resize_horizontal: false,
    plugin_insertdate_dateFormat: "%Y-%m-%d",
    plugin_insertdate_timeFormat: "%H:%M:%S",
    accessibility_focus: false,

    external_link_list_url: "javascript/blog_link_list.js",
    external_image_list_url: "javascript/blog_image_list.js",
    flash_external_list_url: "javascript/blog_flash_list.js",
    media_external_list_url: "javascript/blog_media_list.js",
    entities: "",

    relative_urls: false,

    nonbreaking_force_tab: true,
    apply_source_formatting: true,
    width: "520px"
});

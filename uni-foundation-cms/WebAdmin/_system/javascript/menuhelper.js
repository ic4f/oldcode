function expand(divid, imageid, linkPrefix) {
    div = document.getElementById(divid);
    image = document.getElementById(imageid);
    if (div.style.display == 'none') {
        image.src = linkPrefix + "_system/images/layout/minus.gif";
        div.style.display = 'inline';
    }
    else {
        image.src = linkPrefix + "_system/images/layout/plus.gif";
        div.style.display = 'none';
    }
}
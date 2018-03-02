function empty(eltid) {
    var elt = document.getElementById(eltid);
    if (elt.value.length == 0) {
        elt.setAttribute("class", "tbxreq");
    }
    else {
        elt.setAttribute("class", "tbxnml");
    }
}
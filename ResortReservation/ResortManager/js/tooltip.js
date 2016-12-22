// create global shortcut
//var $UI = farhorizon.bookingchart;
/**
* Copies all the properties of one object to another object.
* @param {Object} target The receiver of the properties
* @param {Object} src The source of the properties
* @param {Object} def (optional) A second object that will be used for default values if present.
* @return {Object} returns the modified target object
* @member DCI2 apply
*/

//$UI.apply = function(target, src, def) {
//	// apply default properties first
//    if (def) {
//        $UI.apply(target, def);
//    }
//    
//    // now add source properties
//    if (target && src && typeof src == 'object') {
//        for (var p in src) {
//            target[p] = src[p];
//        }
//    }
//    return target;
//};


var TT_MouseX, TT_MouseY;
var TT_TimerID = 0;

function getbrowser() {
    //This puts the browser and platform into the class attribute of HTML tag to allow browser sniffing in CSS.
    var ua = navigator.userAgent.toLowerCase();
    var h = document.getElementsByTagName('html')[0];
    var is = function(t) { return ua.indexOf(t) != -1; };
    //this is modified slightly for original to replace spaces in 2 of the regex tests with \x20 to avoid problems when this is compressed
    var b = (!(/opera|webtv/i.test(ua)) && /msie\x20(\d)/.test(ua)) ? ('ie ie' + RegExp.$1) : is('firefox/2') ? 'gecko ff2' : is('firefox/3') ? 'gecko ff3' : is('gecko/') ? 'gecko' : is('opera/9') ? 'opera opera9' : /opera\x20(\d)/.test(ua) ? 'opera opera' + RegExp.$1 : is('konqueror') ? 'konqueror' : is('applewebkit/') ? 'webkit safari' : is('mozilla/') ? 'gecko' : '';
    var os = (is('x11') || is('linux')) ? ' linux' : is('mac') ? ' mac' : is('win') ? ' win' : '';
    var cls = b + os + ' js';
    if (h.className != cls) {
        h.className += h.className ? ' ' + cls : cls;
    }
    return b;
}

function setDisplayed(el, val) {
    if (typeof val == 'boolean') {
        //TODO: should be using '' instead of 'block', but there some IE6 rendering bugs making that fail
        //in some places.
        val = val ? 'block' : 'none';
    }
    else if (typeof val == 'undefined' || val === null) {
        val = 'none';
    }
    if (typeof el == 'string') {
        el = $get(el);
    }
    if (el) {
        el.style.display = val;
    }
    return el;
}


function getChildNodes(el) {
    if (!el || !el.childNodes) {
        return;
    }

    try {
        var arr = [];
        var len = el.childNodes.length;

        for (var i = 0; i < len; i++) {
            if (el.childNodes[i].nodeType == 1) {
                //TODO: this looks wrong - why are we bumping the length before adding.  Creating a 1-based array??? TRR
                arr[arr.length++] = el.childNodes[i];
            }
        }

        return arr;
    }
    catch (ex) { return null; }
}

function initTT() {
    //	var tip = document.createElement("DIV");	
    //	tip.id = "mainTT";
    //	tip.setAttribute("id","mainTT");			
    //	document.getElementsByTagName("body")[0].appendChild(tip);	
    var tip = document.getElementById("mainTT");

    if (document.getElementById("mainTT").hasChildNodes()) {
        while (document.getElementById("mainTT").childNodes.length >= 1) {
            document.getElementById("mainTT").removeChild(document.getElementById("mainTT").firstChild);
        }
    }

    var wh = document.createElement("div"); var h = document.createElement("b"); h.id = "tl";
    var wc = document.createElement("div"); var c = document.createElement("span"); c.id = "tl";
    var wx = document.createElement("div"); var x = document.createElement("input"); x.id = "TT_Exit";

    x.src = '/content/images/Phase2_buttons_X_On.gif';
    x.src = '../images/dateicon.jpg';
    x.type = 'image';
    x.style.display = "none";
    x.style.zIndex = "100012";

    c.style.paddingRight = "20px";

    //    try {    
    //	    $addHandler(x, "click", hideBubbleTooltip);
    //	}
    //	catch {}

    wx.style.height = "12px";
    wx.style.width = "12px";
    wx.style.left = "250px";
    wx.style.top = "0px";
    wx.style.position = "absolute";
    wx.style.padding = "7px 0px 0px 5px";

    wh.appendChild(h);
    //tip.appendChild(wh);
    document.getElementById("mainTT").appendChild(wh);
    wc.appendChild(c);
    wx.appendChild(x);
    wc.appendChild(wx);
    //tip.appendChild(wc);
    document.getElementById("mainTT").appendChild(wc);

    wh.appendChild(h);
    //tip.appendChild(wh);
    document.getElementById("mainTT").appendChild(wh);
    wc.appendChild(c);
    wx.appendChild(x);
    wc.appendChild(wx);
    //tip.appendChild(wc);
    document.getElementById("mainTT").appendChild(wc);


    // this code is only for IE6 and under so that Dropdown List boxes do not show thorugh popups
    //if ($UI.isIE6) {
    var browser = getbrowser();
    if (browser.substring(3, 6) == 'ie6') {
        var wf = document.createElement("div");
        var mainIF = document.createElement("iframe");
        mainIF.id = "tt_iframe";
        mainIF.src = "javascript:'<html></html>'";
        mainIF.marginwidth = "0";
        mainIF.marginheight = "0";
        mainIF.scrolling = "no";
        mainIF.frameborder = "0";
        mainIF.style.height = "0";
        mainIF.style.width = "0";
        mainIF.style.filter = "mask";
        mainIF.style.zIndex = "-1";

        if (!wf.addEventListener) {
            //tip.appendChild(mainIF);
            document.getElementById("mainTT").appendChild(mainIF);
        }
    }
    attachAllTT();
}

function attachAllTT() {
    var arr = getElementsByAttribute("BubbleToolTip", "tooltiped");
    var len = arr.length;
    for (var i = 0; i < len; i++) {
        attachTT(arr[i]);
    }
}

function attachTT(el, arg1) {
    try {
        if (!!el.id) {
            var tgt = $get(el.id);
            if (tgt) {
                //if ($UI.isIE) {
                var browser = getbrowser();
                if (browser.substring(0, 2) == 'ie') {
                    tgt.attachEvent('onmouseout', hideBubbleTooltip);
                }
                else {
                    tgt.addEventListener('mouseout', hideBubbleTooltip, false);
                }
            }
        }
        el.onclick = onclickTT;
    }
    catch (ex) { }
}


function onclickTT(evt) {
    // force hide any tooltip that may be displayed
    hideBubbleTooltip();

    var e = evt || event;
    var o = e.target || e.srcElement;
    if (o.nodeType == 3) {
        o = o.parentNode;
    }

    // Kill the mouseout event for a button click and get the current mouse position on click
    var el = $get(o.id);
    if (el) {
        //if ($UI.isIE) {
        var browser = getbrowser();
        if (browser.substring(0, 2) == 'ie') {
            el.detachEvent('onmouseout', hideBubbleTooltip);
        }
        else {
            el.removeEventListener("mouseout", hideBubbleTooltip, false);
        }
    }

    try {
        pos = findPos(o);
        TT_MouseX = pos[1];
        TT_MouseY = pos[0];
    }
    catch (ex) { }

    return (arguments.length > 1) ? arguments[1] : false;
}

function findPos(obj) { // returns the absolute position of an object
    var l = 0, t = 0;
    if (obj.offsetParent) {
        l = obj.offsetLeft;
        t = obj.offsetTop;
        while (obj.offsetParent) {
            obj = obj.offsetParent;
            l += obj.offsetLeft;
            t += obj.offsetTop;
        }
    }
    return [l, t];
}

function changeDirTT(arg) {
    var arr = getChildNodes($get("mainTT"));
    arr[0].firstChild.id = arg;
    arr[1].firstChild.id = arg;
}

function showBubbleTooltip(arg1, arg2, el, left, top) {
    initTT();
    var mainTT = $get("mainTT");

    var arr = getChildNodes(mainTT);
    arr[0].firstChild.innerHTML = arg1;
    arr[1].firstChild.innerHTML = arg2;
    mainTT.style.position = "absolute";
    mainTT.style.display = "block";
    mainTT.style.zIndex = 100;

    var browser = getbrowser();
    if (browser.substring(0, 5) == 'gecko') {
        left = left + 25;
        top = top + 50;
    }

    if (top > 300) {
        top = top - 200;
        left = left + 25;
    }

    if (left > 700) {        
        left = left - 350;
    }
    
    if (arr[2]) {
        arr[2].style.display = "block";
        arr[2].style.position = "absolute";
        arr[2].style.width = mainTT.offsetWidth + "px";
        arr[2].style.height = mainTT.offsetHeight + "px";
        arr[2].style.left = "0px";
        arr[2].style.top = "0px";
        arr[2].style.border = "solid 0px black";
        arr[2].style.zIndex = 50000;
    }
    startBubbleToolTipTimer(200000, true);
    moveBubbleTooltip(el, left, top);
}

function startBubbleToolTipTimer(timerLength, IsOverlay) {

    var mainTT = $get("mainTT");
    var offSetHoriz, offSetVert;
    $get("TT_Exit").style.display = "block";

    //if ($UI.isIE6) {
    var browser = getbrowser();
    if (browser.substring(3, 6) == 'ie6') {
        $get("tt_iframe").style.zIndex = "-1";
    }

    try {
        var ScrollTop = document.body.scrollTop;
        var ScrollLeft = document.body.scrollLeft;
        if (ScrollTop == 0) {
            if (window.pageYOffset) {
                ScrollTop = window.pageYOffset;
            }
            else {
                ScrollTop = (document.body.parentNode) ? document.body.parentNode.scrollTop : 0;
            }
        }

        if (ScrollLeft == 0) {
            if (window.pageXOffset) {
                ScrollLeft = window.pageXOffset;
            }
            else {
                ScrollLeft = (document.body.parentNode) ? document.body.parentNode.scrollLeft : 0;
            }
        }

        if (TT_MouseY <= 275) {
            TT_MouseY = TT_MouseY + 275;
        }
        else if (TT_MouseY > 275) {
            TT_MouseY = TT_MouseY - 275 + (IsOverlay ? ScrollLeft : 0);
        }

        var oOffH = mainTT.offsetHeight;
        if (TT_MouseX < oOffH) {
            TT_MouseX = TT_MouseX; // + ScrollTop;
        }
        else if (TT_MouseX >= oOffH) {
            TT_MouseX = TT_MouseX - oOffH + (IsOverlay ? ScrollTop : 0);
        }

        mainTT.style.left = TT_MouseY + "px";
        mainTT.style.top = TT_MouseX + "px";

        mainTT.style.display = "block";

    } catch (ex) { }

    TT_TimerID = setTimeout("hideBubbleTooltip(0)", timerLength);
}

function hideBubbleTooltip() {
    try { $get("mainTT").style.display = "none"; } catch (ex) { }
    try {
        var arr = getChildNodes($get("mainTT"));
        if (arr[2]) {
            arr[2].style.display = "none";
            arr[2].style.width = 0;
            arr[2].style.height = 0;
        }
    }
    catch (ex) { }

    if (arguments.length > 0 && TT_TimerID > 0) {
        TT_TimerID = 0;
    }
}

function moveBubbleTooltip(e, left, top) {
    var mainTT = $get('mainTT');
    var posx = 0, posy = 0;

    //$UI.setDisplayed('TT_Exit', false);
    //$UI.setDisplayed(mainTT, true);
    setDisplayed('TT_Exit', false);
    setDisplayed(mainTT, true);

    e = e || window.event;
    if (left || top) {
        posx = left;
        posy = top;
    }
    else if (e.offsetLeft || e.offsetTop) {
        posx = e.offsetLeft;
        posy = e.offsetTop;
    }
    else if (e.pageX || e.pageY) {
        posx = e.pageX;
        posy = e.pageY;
    }
    else if (e.clientX || e.clientY) {
        if (document.documentElement.scrollTop) {
            posx = e.clientX + document.documentElement.scrollLeft;
            posy = e.clientY + document.documentElement.scrollTop;
        }
        else {
            posx = e.clientX + document.body.scrollLeft;
            posy = e.clientY + document.body.scrollTop;
        }
    }

    var o = window.addEventListener ? e.target : event.srcElement;

    mainTT.style.display = 'block';
    mainTT.style.top = posy + 'px';
    mainTT.style.left = posx + 'px';

    try { mainTT.style.left = (orientTT(o, posx, posy)[0] - 20) + 'px'; } catch (ex) {}
    try { mainTT.style.top = (orientTT(o, posx, posy)[1] + 10) + 'px'; } catch (ex) {}
}

function orientTT(o, posx, posy) {
    if (o.getAttribute("Orient") == null && o.parentNode.getAttribute("Orient") == null && o.parentNode.parentNode.getAttribute("Orient") == null) {
        if (posx > getTruePageWidth() - 200) {
            if (posy > getTruePageHeight() - getDimensions($get("mainTT")).height - 15) {
                changeDirTT("br");
                posy -= getDimensions($get("mainTT")).height + 15;
                posx -= 150;
            }
            else {
                changeDirTT("tr");
                posx -= 150;
            }
        }
        else if (posx < getTruePageWidth() - 170) {
            if (posy > getTruePageHeight() - getDimensions($get("mainTT")).height - 15) {
                changeDirTT("bl");
                posy -= getDimensions($get("mainTT")).height + 15;
            }
            else {
                changeDirTT("tl");
            }
        }
    }
    else {
        var Orient = o.getAttribute("Orient");
        if (Orient == null) {
            Orient = o.parentNode.getAttribute("Orient");
        }
        if (Orient == null) {
            Orient = o.parentNode.parentNode.getAttribute("Orient");
        }
        switch (parseInt(Orient, 10)) {
            case 1:
                changeDirTT("bl"); posy -= getDimensions($get("mainTT")).height + 15;
                break;
            case 2:
                changeDirTT("br"); posy -= getDimensions($get("mainTT")).height + 15; posx -= 210;
                break;
            case 3:
                changeDirTT("tr"); posx -= 210;
                break;
            case 4:
                changeDirTT("tl");
                break;
            default:
                break;
        }
    }
    if (posx < 25) {
        posx = 25;
    }

    var w = getTruePageWidth() - 200;
    if (posx > w) {
        posx = w;
    }

    return [posx, posy];
}

function getElementsByAttribute(attr, val) {
    var all = document.all || document.getElementsByTagName('*');
    var arr = [];
    for (var k = 0; k < all.length; k++) {
        if (all[k].getAttribute(attr) == val) {
            arr[arr.length] = all[k];
        }
    }
    return arr;
}

function getTruePageWidth() {
    var docWidth = -20;
    if (typeof window.innerWidth != 'undefined') {
        docWidth += window.innerWidth;
    }
    else if (typeof document.width != 'undefined') {
        docWidth += document.width;
    }
    else if (document.compatMode && document.compatMode != 'BackCompat') {
        docWidth += document.documentElement.clientWidth;
    }
    else if (document.body && typeof document.body.scrollWidth != 'undefined') {
        docWidth += document.body.scrollWidth;
    }

    return docWidth;
}

function getTruePageHeight() {
    var docHeight;
    if (typeof window.innerHeight != 'undefined') {
        docHeight = window.innerHeight;
    }
    else if (typeof document.height != 'undefined') {
        docHeight = document.height;
    }
    else if (document.compatMode && document.compatMode != 'BackCompat') {
        docHeight = document.documentElement.clientHeight;
    }
    else if (document.body && typeof document.body.scrollHeight != 'undefined') {
        docHeight = document.body.scrollHeight;
    }

    return docHeight;
}

function getDimensions(el) {
    dimensions = {
        width: null,
        height: null
    };
    if (document.all) {
        dimensions.width = parseInt(el.clientWidth, 10);
        dimensions.height = parseInt(el.clientHeight, 10);
    }
    else {
        var cs = document.defaultView.getComputedStyle(el, '');
        dimensions.width = parseInt(cs.getPropertyValue('width'), 10);
        dimensions.height = parseInt(cs.getPropertyValue('height'), 10);
    }
    return dimensions;
}

function AICMT(e) {
    moveBubbleTooltip(e);
}

function AICHT() {
    hideBubbleTooltip();
}

function AICST(arg1, arg2) {
    showBubbleTooltip(arg1, arg2);
}
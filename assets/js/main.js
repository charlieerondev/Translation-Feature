$(document).ready(function(){
	Translate();
});

function Translate() {
var clsElements = document.querySelectorAll(".e-s");
    var keys = [];
    var path = window.location.href.split(window.location.host + "/")[1];
    for (var i = 0, max = clsElements.length; i < max; i++) {
        var element = clsElements[i];
        var key;
        var type = "langString";
        element.setAttribute("data-translation-id", i);
        if (element.getAttribute("data-htmlblock-id")) {
            key = path + "_" + element.getAttribute("data-htmlblock-id");
            type = "htmlBlock";
        }
        else if (element.getAttribute("placeholder")) { key = element.getAttribute("placeholder").trim(); }
        else if (element.getAttribute("value")) { key = element.getAttribute("value").trim(); }
        else { key = element.innerText.trim(); }

        keys.push({
            key: key,
            lang: lang,
            id: i,
            type: type
        });
    }
    (function () {
        $.ajax({
            type: "POST",
            url: "/WebMethods.aspx/GetTranslation",
            data: JSON.stringify({ input: JSON.stringify(keys) }),
            contentType: "application/json",
            dataType: "json"
        })
            .done(function (data) {
                var results = JSON.parse(data.d);
                var missingTranslations = [];
                for (var i = 0, max = results.length; i < max; i++) {
                    if (results[i].translation.length > 0) {
                        var translationId = results[i].id;
                        var translation = results[i].translation;

                        var element = document.querySelector("[data-translation-id='" + translationId + "']");
                        var jElement = $("[data-translation-id='" + translationId + "']");
                        if (jElement.hasClass("htmlBlock")) {
                            jElement.html(translation);
                        }
                        else {
                            if (element.getAttribute("placeholder")) {
                                $("#" + element.getAttribute("id")).attr("placeholder", translation);
                            } else if (element.innerText) {
                                element.innerText = translation;
                            } else {
                                $("[data-translation-id='" + translationId + "']").attr("value", translation);
                            }
                        }
                    } else {
                        var key = results[i].key;
                        if (missingTranslations.indexOf(key) === -1) {
                            missingTranslations.push(key);
                        }
                    }
                }
                if (missingTranslations.length > 0 && (window.location.href.indexOf("aws.glic.com") > -1 || window.location.href.indexOf("localhost") > -1)) {
                    console.warn("missing translations:");
                    console.warn(missingTranslations);
                }
                try {
                    //CERON 20180927 -- For related scripts in Child pages in ScriptsContent block 
                    postFill();
                } catch(e){
                }
            })
            .fail(function (xhr) {
                console.error("oops: " + xhr);
            });
    })();
}
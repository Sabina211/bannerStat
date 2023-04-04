import { createUUID } from "./myFunc.js";
import { getCookie } from "./myFunc.js";

var bannerIds = document.querySelectorAll('.banner');
var bannerBlock = document.getElementsByClassName("banner");
console.info(document.cookie);

var clientUserId = getCookie('clientUserId');
if (clientUserId == undefined) {
    clientUserId = createUUID();
    document.cookie = `clientUserId=${clientUserId}; Max-Age=2628000; path=/;`;
}

async function sendAction(actionType, bannerId) {
    let url = 'https://sark.ws/api/userAction';
    let data;
    data = {
        'bannerId': `${bannerId}`,
        'clientUserId': `${clientUserId}`,
        'sourceUrl': document.URL,
        'actionType': actionType,
    }

    let res = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data),
    });

    if (res.ok) {
        console.info(`sendAction ${actionType} is executed`);
    } else {
        console.error("sendAction is failed")
    }
}


async function getUrl(url) {
    var response = await fetch(url);
    var bannerUrl = await response.text();
    if (response.ok) {
        console.info(`bannerUrl=  ${bannerUrl}`);
    } else {
        console.error("bannerUrl is failed")
    }
    return bannerUrl;
}

for (let i = 0; i < bannerIds.length; i++) {
    await sendAction(0, bannerBlock[i].id); //отправляем данные о просмотре страницы
    await new Promise(r => setTimeout(r, 1000));
}


async function replaceBanner() {
    var bannerBlock = document.getElementsByClassName("banner");
    for (let i = 0; i < bannerBlock.length; i++) {
        var bannerLink = await getUrl(`https://sark.ws/api/userAction/${bannerBlock[i].id}`);
        var newContent = `<div id = "${bannerBlock[i].id}"  class="image-group banner"><a href="${bannerLink}" target="_blank"><img class="banner_img" src="https://sark.ws/api/userAction/img/${bannerBlock[i].id}?clientUserId=${clientUserId}" alt="Баннер с рекламой"/></a></div>`;
        $(bannerBlock[i]).replaceWith(newContent);
    }
}

await replaceBanner();
await clickBanner();

async function clickBanner() {
    var divs = document.querySelectorAll('.banner');
    for (let i = 0; i < divs.length; i++) {
        var img = divs[i].querySelectorAll('img')[0];
        img.addEventListener('click', function (e) {
            sendAction(2, divs[i].id); //отправляем данные о клике на баннер
        });
    }
};

var os = new OnScreen(100, 0)
var divs = document.querySelectorAll('.banner');
divs.forEach(function (div, k) {
    var img = div.querySelectorAll('img')[0];
    var imgSelector = 'img[src="' + img.src + '"]'
    var nth = k + 1
    os.on('enter', imgSelector, function (element, event) {
        console.log('on enter =>', element)
        sendAction(1, div.id); //оправляем данные о просмотре баннера
        os.off('enter', imgSelector, 'anonymous')
    })
})
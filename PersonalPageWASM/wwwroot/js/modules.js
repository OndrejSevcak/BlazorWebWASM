export function enableCodeHighlight() {
    document.querySelectorAll("pre code").forEach((elm) => {
        hljs.highlightBlock(elm);
    })
}

export function replaceCssClass(toBeReplaced, replacement) {
    const elements = document.querySelectorAll(`.${toBeReplaced}`);
    elements.forEach((elm) => {
        elm.classList.remove(toBeReplaced);
        elm.classList.add(replacement);
    });
}

export function applyPostStyling() {
    const h3s = document.getElementsByTagName('h3');
    if (h3s) {
        Array.from(h3s).forEach(e => {
            e.style = 'margin-top: 25px;';
        });
    }
}



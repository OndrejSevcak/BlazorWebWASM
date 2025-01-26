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



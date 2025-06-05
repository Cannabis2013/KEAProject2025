export function scroll(height, elementY) {
    const wHeight = window.innerHeight
    let windowScrollY = window.scrollY
    if (elementY <= wHeight / 2)
        windowScrollY -= wHeight - elementY - height
    else
        windowScrollY += elementY - (wHeight - height)
    window.scrollTo(0, windowScrollY)
}

export default scroll
window.scrollFunctions = {
    saveScrollPosition: function () {
        return {
            x: window.scrollX,
            y: window.scrollY
        };
    },
    restoreScrollPosition: function (position) {
        window.scrollTo(position.x, position.y);
    }
};
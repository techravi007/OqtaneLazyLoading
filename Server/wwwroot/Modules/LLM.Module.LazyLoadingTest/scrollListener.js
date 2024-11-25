window.scrollListener = {
    addScrollListener: function (dotnetReference) {
        window.addEventListener('scroll', function () {
            if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight - 100) {
                dotnetReference.invokeMethodAsync('LoadMoreItems');
            }
        });
    }
};
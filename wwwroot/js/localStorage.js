window.localStorageHelper = {
    setItem: function (key, value) {
        localStorage.setItem(key, JSON.stringify(value));
    },
    getItem: function (key) {
        var value = localStorage.getItem(key);
        return value ? JSON.parse(value) : null;
    }
};

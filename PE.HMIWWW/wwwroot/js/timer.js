function Timer(callback, time) {
    this.setTimeout(callback, time);
}

Timer.prototype.setTimeout = function (callback, time) {
    var self = this;
    if (this.timer) {
        this.cancel();
    }
    this.finished = false;
    this.callback = callback;
    this.time = time;
    this.timer = setTimeout(function () {
        self.finished = true;
        callback(self);
    }, time);
    this.start = Date.now();
}

Timer.prototype.add = function (time) {
    if (!this.finished) {
        // add time to time left
        time = this.time - (Date.now() - this.start) + time;
        this.setTimeout(this.callback, time);
    } else {
        this.setTimeout(this.callback, time);
    }
}

Timer.prototype.reset = function (time) {
    this.setTimeout(this.callback, this.time);
}

Timer.prototype.cancel = function () {
    if (!this.finished) {
        clearTimeout(this.timer);
    }
};

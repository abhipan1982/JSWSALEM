var HmiRefreshTarget;

//$(window).bind("load", function () {
//    HmiRefreshTarget = new EventTarget();
//});
function InitRefreshHandler() {

    try {
      SignalrConnection.on("System2HmiRefresh", (refreshData) => {
            console.log("refresh: " + refreshData.RefreshKey);
            FireRefreshEvent(refreshData.RefreshKey);
        });
  }
  catch (err) {
    console.log(err);
  }
}



function EventTarget() {
  this._listeners = {};
}

EventTarget.prototype = {

  constructor: EventTarget,

  fire: function (refreshKey) {
    //if (typeof parameter == "string") {
    //    parameter = { refreshKey: parameter };
    //}

    if (!refreshKey) {  //falsy
      console.log("Event object missing 'type' property.");
      throw new Error("Event object missing 'type' property.");
    }

    if (this._listeners[refreshKey] instanceof Array) {
      var listeners = this._listeners[refreshKey];

      if (listeners.length > 0) {
        console.log("SYS REFRESH. Key: " + refreshKey + " No of listeners: " + listeners.length);
      }

      for (var i = 0, len = listeners.length; i < len; i++) {
        try {
          listeners[i].call(this, refreshKey);
        }
        catch (ex) {
          console.log("Exception during triggering refresh event with key: " + refreshKey + " : " + ex);
        }
      }
    }
  },
  addListener: function (refreshKey, listener) {
    if (typeof this._listeners[refreshKey] === "undefined") {
      this._listeners[refreshKey] = [];
    }

    this._listeners[refreshKey].push(listener);

    //HmiRefreshTarget.fire(refreshKey);
  },
  hasListener: function (refreshKey) {
    return typeof this._listeners[refreshKey] !== "undefined" && !!this._listeners[refreshKey].length;
  },
  removeListener: function (refreshKey) {
    this._listeners[refreshKey] = [];
  },
  listRegisteredKeys: function () {
    return Object.keys(this._listeners);
  }
};


function PrepareRefreshEvent() {
  HmiRefreshTarget = new EventTarget();
}
 /** Register refresh key before page load event */
function RegisterMethod(refreshKey, methodToCall) {
  if (HmiRefreshTarget === undefined)
    PrepareRefreshEvent();
  HmiRefreshTarget.addListener(refreshKey, methodToCall);
}
function ReRegisterMethod(refreshKey, methodToCall) {
    if (HmiRefreshTarget === undefined)
        PrepareRefreshEvent();
    if (HmiRefreshTarget.hasListener(refreshKey))
        HmiRefreshTarget.removeListener(refreshKey);
    HmiRefreshTarget.addListener(refreshKey, methodToCall);
}
function FireRefreshEvent(refreshKey) {
  if (HmiRefreshTarget === undefined)
    PrepareRefreshEvent();
  HmiRefreshTarget.fire(refreshKey);
}

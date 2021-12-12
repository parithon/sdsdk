var websocket = null,
    actionInfo = {},
    info = {},
    uuid = null,
    globalSettings = {};

function registerPluginOrPI(inEvent, inUUID) {
    if (websocket) {
        var json = {
            event: inEvent,
            uuid: inUUID
        };
        websocket.send(JSON.stringify(json));
    }
}

function requestGlobalSettings(inUUID) {
    if (websocket) {
        var json = {
            event: "getGlobalSettings",
            context: inUUID
        };
        websocket.send(JSON.stringify(json));
    }
}

function saveGlobalSettings(inUUID) {
    if (websocket) {
        var json = {
            event: "setGlobalSettings",
            context: inUUID,
            payload: globalSettings
        };
        websocket.send(JSON.stringify(json));
    }
}

function sendToPlugin() {
    console.log(`sendToPlugin: clicked`);
    var json = {
        action: actionInfo.action,
        event: "sendToPlugin",
        context: uuid,
        payload: {
            command: "refresh"
        }
    };
    websocket.send(JSON.stringify(json));
}

function connectElgatoStreamDeckSocket(inPort, inUUID, inRegisterEvent, inInfo, inActionInfo) {
    actionInfo = JSON.parse(inActionInfo);
    info = JSON.parse(inInfo);
    uuid = inUUID;

    websocket = new WebSocket(`ws://127.0.0.1:${inPort}`);

    websocket.onopen = () => {
        registerPluginOrPI(inRegisterEvent, inUUID);
        requestGlobalSettings(inUUID);
    }

    websocket.onmessage = (evt) => {
        var jsonObj = JSON.parse(evt.data);
        var event = jsonObj["event"];
        var jsonPayload = jsonObj["payload"];

        console.log(`onmessage: ${evt}`);

        if (event === "didReceiveGlobalSettings") {
            globalSettings = jsonPayload["settings"];
            console.log(`didReceiveGlobalSettings: ${globalSettings}`);
        }

        if (event === "sendToPropertyInspector") {
            console.log(`sendToPropertyInspector: ${jsonPayload}`);
        }
    }
}

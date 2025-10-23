async function initMapForAdList(adListForMap, beautifulBaierbrunn, toBaierbrunn, fromBaierbrunn, noType, driver, passenger, adData, direction, type, pickUp, location, seats, clickHere) {

    const { Map, InfoWindow } = await google.maps.importLibrary("maps");
    const { AdvancedMarkerElement, PinElement } = await google.maps.importLibrary(
        "marker",
    );
    const map = new Map(document.getElementById("map"), {
        zoom: 9,
        center: { lat: 48.0206345131560, lng: 11.482919688669709 },
        mapId: "4504f8b37365c3d0",
    });

    const BBMarker =
        [
            {
                position: { lat: 48.0206345131560, lng: 11.482919688669709 },
                title: beautifulBaierbrunn,
            },
        ];

    adListForMap.forEach(ad => {

        let adDirection = "";
        if (ad.adDirection === 0) {
            adDirection = toBaierbrunn;
        }
        if (ad.adDirection === 1) {
            adDirection = fromBaierbrunn;
        }

        let adType = "";
        if (ad.adType === 0) {
            adType = noType;
        }
        if (ad.adType === 1) {
            adType = driver;
        }
        if (ad.adType === 2) {
            adType = passenger;
        }

        const html = `
  <div>
    <h3>${adData}</h3>
    <ul>
      <li>${direction} ${adDirection}</li>
      <li>${type} ${adType}</li>
      <li>${pickUp} ${ad.pickUpDateAndTime}</li>
      <li>${location} ${ad.pickUpDropOffLocation}</li>
      <li>${seats} ${ad.numberOfSeats}</li>
      <li><a href="http://localhost:5049/ShowAd/${ad.id}" target="_blank">${clickHere}</a></li>
    </ul>
  </div>
`;
        BBMarker.push(
            {
                position: { lat: Number(ad.latitude), lng: Number(ad.longitude) },
                title: html,
            }
        )
    });

    // Create an info window to share between markers.
    const infoWindow = new InfoWindow();

    // Create the markers.
    BBMarker.forEach(({ position, title }, i) => {
        const pin = new PinElement({
            /*glyph: `${i}`,*/
            scale: 1.5,
        });
        // [START maps_advanced_markers_accessibility_marker]
        const marker = new AdvancedMarkerElement({
            position,
            map,
            title: `${title}`,
            content: pin.element,
            gmpClickable: true,
        });



        // [END maps_advanced_markers_accessibility_marker]
        // [START maps_advanced_markers_accessibility_event_listener]
        // Add a click listener for each marker, and set up the info window.
        marker.addListener("click", ({ domEvent, latLng }) => {
            const { target } = domEvent;

            infoWindow.close();
            infoWindow.setContent(marker.title);
            infoWindow.open(marker.map, marker);
        });
        // [END maps_advanced_markers_accessibility_event_listener]
    });

}

let latitude = 0;
let longitude = 0;

async function initMapForCreatingAd(lat, long, instruction, pinDroppedAt) {
    // Request needed libraries.
    const { Map, InfoWindow } = await google.maps.importLibrary("maps");
    const { AdvancedMarkerElement } = await google.maps.importLibrary("marker");
    const map = new Map(document.getElementById("map"), {
        center: { lat: lat, lng: long },
        zoom: 12,
        mapId: "4504f8b37365c3d0",
    });
    const infoWindow = new InfoWindow();
    const draggableMarker = new AdvancedMarkerElement({
        map,
        position: { lat: lat, lng: long },
        gmpDraggable: true,
        title: instruction,
    });

    draggableMarker.addListener("dragend", (event) => {
        const position = draggableMarker.position;

        infoWindow.close();
        infoWindow.setContent(`${pinDroppedAt} ${position.lat}, ${position.lng}`);
        infoWindow.open(draggableMarker.map, draggableMarker);
        latitude = position.lat;
        longitude = position.lng;
    });
}

async function initMapForOnePin(comment, latitude, longitude) {

    const { Map, InfoWindow } = await google.maps.importLibrary("maps");
    const { AdvancedMarkerElement, PinElement } = await google.maps.importLibrary(
        "marker",
    );
    const map = new Map(document.getElementById("map"), {
        zoom: 9,
        center: { lat: latitude, lng: longitude },
        mapId: "4504f8b37365c3d0",
    });

    const BBMarker =
        [
            {
                position: { lat: latitude, lng: longitude },
            },
        ];

    // Create the markers.
    BBMarker.forEach(({ position, title }, i) => {
        const pin = new PinElement({
            /*glyph: `${i}`,*/
            scale: 1.5,
        });
        // [START maps_advanced_markers_accessibility_marker]
        const marker = new AdvancedMarkerElement({
            position,
            map,
            title: `${comment}`,
            content: pin.element,
            gmpClickable: true,
        });
    });

    // Create an info window to share between markers.
    const infoWindow = new InfoWindow();
}





function getLatitude() {
    return latitude;
}

function getLongitude() {
    return longitude;
}
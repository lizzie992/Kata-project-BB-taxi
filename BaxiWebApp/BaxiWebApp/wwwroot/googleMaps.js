async function initMapForAdList(adListForMap) {

    // Request needed libraries.
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
                title: "Beautiful Baierbrunn",
            },
        ];

    adListForMap.forEach(ad => {

        let direction = "";
        if (ad.adDirection === 0) {
            direction = "To Baierbrunn";
        }
        if (ad.adDirection === 1) {
            direction = "From Baierbrunn";
        }

        let type = "";
        if (ad.adType === 0) {
            type = "No";
        }
        if (ad.adType === 1) {
            type = "Driver";
        }
        if (ad.adType === 2) {
            type = "Passenger";
        }

        const html = `
  <div>
    <h3>Ad Overview</h3>
    <ul>
      <li>Direction: ${direction}</li>
      <li>Type: ${type}</li>
      <li>Pickup: ${ad.pickUpDateAndTime}</li>
      <li>Location: ${ad.pickUpDropOffLocation}</li>
      <li>Seats: ${ad.numberOfSeats}</li>
      <li><a href="http://localhost:5049/ShowAd/${ad.id}" target="_blank">Click here...</a></li>
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

async function initMapForCreatingAd(lat, long) {
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
        title: "Please move this marker to the exact pick up / drop off location",
    });

    draggableMarker.addListener("dragend", (event) => {
        const position = draggableMarker.position;

        infoWindow.close();
        infoWindow.setContent(`Pin dropped at: ${position.lat}, ${position.lng}`);
        infoWindow.open(draggableMarker.map, draggableMarker);
        latitude = position.lat;
        longitude = position.lng;
    });
}

function getLatitude() {
    return latitude;
}

function getLongitude() {
    return longitude;
}


function scrollToBottom() {
    const block = document.querySelector(".message-block");
    block.scrollTop = block.scrollHeight;
}

function scrollToBottomPhone() {
    const block = document.querySelector(".message-block-phone");
    block.scrollTop = block.scrollHeight;
}
async function initMap(adListForMap) {

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

    adListForMap.forEach(
        ad => BBMarker.push(
            {
                position: { lat: Number(ad.latitude), lng: Number(ad.longitude) },
                title: ad.pickUpDropOffLocation,
            }
        )
    );

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

// [END maps_advanced_markers_accessibility]



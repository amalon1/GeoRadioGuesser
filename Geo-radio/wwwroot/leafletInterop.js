window.leafletInterop = {
    map: null,

    InitMap: function(dotNetReference) {
        if (this.map) {
            this.map.remove();
        }

        this.map = L.map("map");

        this.map.createPane('labels');

        this.map.getPane("labels").style.zIndex = 650;
        this.map.getPane("labels").style.pointerEvents = "none";

        var MapComponent = L.tileLayer("https://{s}.basemaps.cartocdn.com/light_nolabels/{z}/{x}/{y}{r}.png", {
            attribution: "©OpenStreetMap, ©CartoDB"
        }).addTo(this.map);

        var MapLabels = L.tileLayer("https://{s}.basemaps.cartocdn.com/light_only_labels/{z}/{x}/{y}.png", {
            attribution: "©OpenStreetMap, ©CartoDB",
            pane: "labels"
        }).addTo(this.map);

        var geojson = L.geoJson(window.WorldCountries, {onEachFeature: function (feature, layer) {
                layer.on("click", function () {
                    dotNetReference.invokeMethodAsync("OnCountryClicked", feature.id);
                });
            }
        }).addTo(this.map);

        const bounds = geojson.getBounds();

        this.map.fitBounds(bounds);
        this.map.setMaxBounds(bounds);
        this.map.setMinZoom(2);
        this.map.options.maxBoundsViscosity = 1.0;
    }
}

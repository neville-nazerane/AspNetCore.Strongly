
function stronglySent(path) {
    buildBindings();
    $.post(path, JSON.stringify(stronglyData), f => console.log(f), "json");
}

function buildBindings() {
    for (var a in stronglyData.bindings) {
        var selected = $('[binding-key="' + a + '"]');
        if (selected.length > 0) {
            var result = { };
            $.each(selected.serializeArray(), function () {
                result[this.name] = this.value;
            });
            stronglyData.bindings[a].data = JSON.stringify(result);
        }
    }
}
window.onload = function () {
	fetchGet("Grafico/graficoInicial", "text", function (data) {
		document.getElementById("imgFoto").src = "data:image/png;base64," + data;
	})

	fetchGet("Grafico/graficoInicial1", "text", function (data) {
		document.getElementById("imgFoto1").src = "data:image/png;base64," + data;
	})
}
window.onload = function () {
	fetchGet("Grafico/graficoBarras", "text", function (data) {
		document.getElementById("imgFoto3").src = "data:image/png;base64," + data;
	})
}

function DescargarGraficoImagen() {
	descargarImagen("imgFoto3","GraficoBarras");
}
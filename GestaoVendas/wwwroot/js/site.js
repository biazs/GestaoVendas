// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function AtualizaMascaraValor() {

    if (document.getElementById("PrecoUnitario").value.indexOf(".") == -1 && document.getElementById("PrecoUnitario").value.indexOf(",") == -1) // nao existe ponto, nem virgula
    {
        var resultado = document.getElementById("PrecoUnitario").value + ".00";
        document.getElementById("PrecoUnitario").value = resultado;
    }

    else {

        /* SE existir somente um caracter depois do ponto - preencher com .X0 */
        var temPonto = document.getElementById("PrecoUnitario").value.substr(-2);
        if (temPonto.indexOf(".") == 0 || temPonto.indexOf(",") == 0) {
            var resultado = document.getElementById("PrecoUnitario").value + "0";
            resultado = resultado.replace(",", ".");
            document.getElementById("PrecoUnitario").value = resultado;
        }
        else {
            var preco = document.getElementById("PrecoUnitario").value.replace(',', '');
            var preco = preco.replace('.', '');
            var tam = preco.length - 2;
            var resultado = preco.substr(0, tam) + "." + preco.substr(-2);

            document.getElementById("PrecoUnitario").value = resultado;
        }
    }
}
   
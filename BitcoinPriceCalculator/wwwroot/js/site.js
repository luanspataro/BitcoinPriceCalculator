    document.getElementById("bitcoinForm").addEventListener("submit", function (event) {
    event.preventDefault();

    var form = event.target;
    var formData = new FormData(form);
    var jsonData = { };

    formData.forEach((value, key) => {
        jsonData[key] = value;
    });

    fetch("/Bitcoin/GetBitcoinPrice", {
        method: "POST",
    headers: {
        "Content-Type": "application/json"
        },
        body: JSON.stringify(jsonData)
    })
    .then(response => response.json())
    .then(data => {
        var resultDiv = document.getElementById("result");
        if (data.success) {
            resultDiv.innerHTML = `
            <div class="mb-0">
                <h4 class="mt-3">Quantidade comprada</h4>
                <p>
                ${data.amount} BTC
                </p>

            <div class="row justify-content-center">
                <div class="col-4">
                    <h4 class="">Valorização</h4>
                        <p class="${data.percentage >= 0 ? 'profit' : 'loss'}">
                            ${data.percentage.toFixed(2)} %
                        </p>
                </div>

                <div class="col-4">
                    <h4 class="">Lucro R$</h4>
                        <p class="${data.percentage >= 0 ? 'profit' : 'loss'}">
                            ${data.profit}
                        </p>
                    </div>
                </div>

            </div>
            <div class="d-flex justify-content-center gif">
                <script src="https://unpkg.com/@dotlottie/player-component@latest/dist/dotlottie-player.mjs" type="module"></script>

                <dotlottie-player src="https://lottie.host/fbb4a9d1-1d24-4070-9e6a-0aedc8dd7837/sNJww6gjga.json" background="transparent" speed="1" style="width: 200px; height: 230px;" loop autoplay></dotlottie-player>
            </div>`;
            } else {
                resultDiv.innerHTML = `<p class="text-danger">Erro: ${data.error}</p>`;
            }
        })
        .catch(error => {
        console.error("Erro:", error);
    });
    });
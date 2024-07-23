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
        resultDiv.innerHTML = `<div>
<h4 id="btc-title" class="mt-3">Preço do Bitcoin:</h4>
    <p id="btc-title">
        ${data.price}
    </p>
</div>
<iframe class="mb-0" src="https://lottie.host/embed/ebe1e058-da64-403f-a849-f6c1926c277c/aipyC1JMq7.json"></iframe>`;
                                    } else {
        resultDiv.innerHTML = `<p class="text-danger">Erro: ${data.error}</p>`;
                                    }
                                })
                                .catch(error => {
        console.error("Erro:", error);
                                });
                        });
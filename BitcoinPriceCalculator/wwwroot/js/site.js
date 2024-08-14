document.getElementById("bitcoinForm").addEventListener("submit", function (event) {
    event.preventDefault();

    var form = event.target;
    var formData = new FormData(form);
    var jsonData = {};

    formData.forEach((value, key) => {
        jsonData[key] = value;
    });

    var submitButton = form.querySelector('button[type="submit"]');
    submitButton.disabled = true;

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
                const element = document.querySelector('.element');
                const animationIframe = document.createElement('iframe');
                animationIframe.src = "https://lottie.host/embed/31e3a97c-2bec-453f-acdf-741f597ed546/9IX32tADqW.json";
                animationIframe.style.width = "55vw";
                animationIframe.style.height = "35vh";
                animationIframe.style.border = "none";
                animationIframe.style.position = "absolute";
                animationIframe.style.top = "50%";
                animationIframe.style.left = "50%";
                animationIframe.style.transform = "translate(-50%, -50%)";
                animationIframe.style.display = "none";
                document.body.appendChild(animationIframe);

                element.classList.add('shrink');

                const resultHTML = `
                <div class="mb-0 result-content">
                    <h4 class="mt-3">Quantidade comprada</h4>
                    <p>${data.amount} BTC</p>

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

                const initialText = document.querySelector('.initial-text');
                initialText.classList.add('move-up');

                setTimeout(() => {
                    animationIframe.style.display = 'block';

                    setTimeout(() => {
                        animationIframe.style.display = 'none';

                        element.classList.remove('shrink');
                        element.classList.add('grow');

                        resultDiv.innerHTML = resultHTML;
                        initialText.classList.add('move-down');

                        setTimeout(() => {
                            element.classList.remove('grow');

                            submitButton.disabled = false;

                        }, 1000);
                    }, 2700);
                }, 500);
            } else {
                resultDiv.innerHTML = `<p class="text-danger">Erro: ${data.error}</p>`;
                submitButton.disabled = false;
            }
        })
        .catch(error => {
            console.error("Erro:", error);
            submitButton.disabled = false;
        });
});

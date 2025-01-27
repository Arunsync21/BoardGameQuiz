document.addEventListener("DOMContentLoaded", () => {
    let timerElement = document.getElementById("time");
    let timeLeft = 30;
    const options = document.querySelectorAll(".option");
    let answered = false; // Flag to track if the user has answered

    // Correct answer index (for demo purposes, assuming it's the first option)
    const correctAnswerIndex = 0;

    // Timer countdown logic
    const countdown = setInterval(() => {
        timeLeft--;
        timerElement.textContent = `00:${timeLeft < 10 ? "0" + timeLeft : timeLeft}`;
        if (timeLeft <= 0) {
            clearInterval(countdown);
            if (!answered) {
                highlightCorrectAnswer();
                displayMessage("Time's up!");
            }
        }
    }, 1000);

    // Event listener for option clicks
    options.forEach((option, index) => {
        option.addEventListener("click", () => {
            if (answered) return; // Prevent multiple answers
            answered = true;
            clearInterval(countdown);

            if (index === correctAnswerIndex) {
                highlightCorrectAnswer();
                displayMessage("Correct!");
            } else {
                highlightCorrectAnswer();
                option.classList.add("wrong");
                displayMessage("InCorrect!");
            }
        });
    });

    // Highlight the correct answer
    function highlightCorrectAnswer() {
        options[correctAnswerIndex].classList.add("correct");
    }

    // Display a message on the screen
    function displayMessage(message) {
        const quizContent = document.querySelector(".quiz-content");
        quizContent.innerHTML = `<h1>${message}</h1>`;
        setTimeout(() => {
            // You can implement logic here to move to the next question or end the quiz
            console.log("Move to next question.");
        }, 2000);
    }
});

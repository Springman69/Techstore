//karuzela do zdjec
let slideIndex = 0;
showSlides(slideIndex);

function moveSlide(n) {
    showSlides(slideIndex += n);
}

function showSlides(n) {
    let i;
    let slides = document.getElementsByClassName("carousel-image");
    if (n >= slides.length) { slideIndex = 0 }
    if (n < 0) { slideIndex = slides.length - 1 }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    slides[slideIndex].style.display = "block";
}

// komunikat
document.getElementById("wyslijWiadomosc").onclick = function (event) {
    if (!confirm('Czy chcesz wysłać?')) {
        event.preventDefault();
        return;
    }

    var form = document.getElementById('review-form');
    if (form.checkValidity()) {
        alert("Twoja opinia została wysłana!");
        location.reload();
    } else {
        alert("Uzupełnij wszystkie wymagane pola :(");
        event.preventDefault();
    }
}
//gwiazdki
document.querySelectorAll('.star').forEach(star => {
    star.addEventListener('click', function () {
        let rating = this.dataset.value;
        let category = this.parentElement.dataset.category;
        updateRating(category, rating);
        if (category === 'quality' || category === 'features') {
            updateOverallRating();
        }
    });
});
document.querySelectorAll('.stars-overview .star').forEach(star => {
    star.addEventListener('click', function () {
        let rating = this.dataset.value;
        let category = this.closest('.stars-overview').dataset.category;
        updateRating(category, rating);
    });
});

function updateRating(category, rating) {
    let stars = document.querySelector(`[data-category="${category}"]`).children;
    for (let i = 0; i < stars.length; i++) {
        stars[i].style.color = i < rating ? 'gold' : 'grey';
    }
    let resultSpan = document.querySelector(`[data-category="${category}"] .rating-result`);
    resultSpan.textContent = `${rating}/5`;

    
    if (category === 'quality') {
        document.getElementById('Quality').value = rating;
    } else if (category === 'features') {
        document.getElementById('Function').value = rating;
    }

    updateOverallRating();
}

function updateOverallRating() {
    let qualityRating = document.getElementById('Quality').value;
    let featuresRating = document.getElementById('Function').value;

    let average = 0;
    if (qualityRating && featuresRating) {
        average = (parseInt(qualityRating) + parseInt(featuresRating)) / 2;
    }
    document.getElementById('Overall').value = average.toFixed(1);
    document.querySelector('.average-rating-result').textContent = `${average.toFixed(1)}/5`;
}

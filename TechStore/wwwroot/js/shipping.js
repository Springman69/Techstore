function toggleFormType(type) {
    var privateForm = document.getElementById('privateForm');
    var companyForm = document.getElementById('companyForm');

    if (type === 'private') {
        privateForm.style.display = 'block';
        companyForm.style.display = 'none';
    } else {
        privateForm.style.display = 'none';
        companyForm.style.display = 'block';
    }
}


document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('addCommentButton').addEventListener('click', function () {
        var commentSection = document.getElementById('commentSection');
        commentSection.style.display = commentSection.style.display === 'block' ? 'none' : 'block';
    });
});

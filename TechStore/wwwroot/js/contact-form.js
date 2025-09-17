document.getElementById("fileInput").addEventListener("change", function () {
    var fileList = this.files;
    var fileDisplayArea = document.getElementById("fileDisplayArea");

    for (var i = 0; i < fileList.length; i++) {
        var file = fileList[i];

        var reader = new FileReader();
        reader.onload = function (e) {
            var img = document.createElement("img");
            img.src = e.target.result;
            img.style.maxWidth = "100px";

            var deleteButton = document.createElement("button");
            deleteButton.textContent = "Usuń";
            deleteButton.addEventListener("click", function () {
                fileDisplayArea.removeChild(img);
                fileDisplayArea.removeChild(deleteButton);
            });

            fileDisplayArea.appendChild(img);
            fileDisplayArea.appendChild(deleteButton);
        };
        reader.readAsDataURL(file);
    }
});

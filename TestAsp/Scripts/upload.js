var data = new FormData();
$(document).ready(function () {
// Весь последующий код javascript здесь
    // В dataTransfer помещаются изображения которые перетащили в область div
    jQuery.event.props.push('dataTransfer');

    // Максимальное количество загружаемых изображений за одни раз
    var maxFiles = 6;

    // Оповещение по умолчанию
    var errMessage = 0;

    // Кнопка выбора файлов
    var defaultUploadBtn = $('#uploadbtn');

    // Массив для всех изображений
    var dataArray = [];

    // Область информер о загруженных изображениях - скрыта
    $('#uploaded-files').hide();
    
    $('#ajax').on('click', '.upl', function () {
        alert('upl');
        upload();
    });

    // При нажатии на кнопку выбора файлов
    defaultUploadBtn.on('change', function () {
        // Заполняем массив выбранными изображениями
        var files = $(this)[0].files;
        // Проверяем на максимальное количество файлов
        if (files.length <= maxFiles) {
            // Передаем массив с файлами в функцию загрузки на предпросмотр
            loadInView(files);
            // Очищаем инпут файл путем сброса формы
            // Или вот так $("#uploadbtn").replaceWith( $("#uploadbtn").val('').clone( true ) );
            $('#frm').each(function () {
                this.reset();
            });

        } else {
            alert('Вы не можете загружать больше ' + maxFiles + ' изображений!');
            files.length = 0;
        }
    });


    // Функция загрузки изображений на предпросмотр
    function loadInView(files) {
        $(document).ready(function () {
            // Показываем обасть предпросмотра
            $('#uploaded-holder').show();

            // Для каждого файла
            $.each(files, function (index, file) {

                // Несколько оповещений при попытке загрузить не изображение
                if (!files[index].type.match('image.*')) {

                    if (errMessage == 0) {
                        $('#drop-files p').html('Эй! только изображения!');
                        ++errMessage
                    }
                    else if (errMessage == 1) {
                        $('#drop-files p').html('Стоп! Загружаются только изображения!');
                        ++errMessage
                    }
                    else if (errMessage == 2) {
                        $('#drop-files p').html("Не умеешь читать? Только изображения!");
                        ++errMessage
                    }
                    else if (errMessage == 3) {
                        $('#drop-files p').html("Хорошо! Продолжай в том же духе");
                        errMessage = 0;
                    }

                } else {
                    data.append("img"+index, file);
                    // Проверяем количество загружаемых элементов
                    //if ((dataArray.length + files.length) <= maxFiles) {
                    //    // показываем область с кнопками
                    //    $('#upload-button').css({ 'display': 'block' });
                    //}
                    //else { alert('Вы не можете загружать больше ' + maxFiles + ' изображений!'); return; }

                    // Создаем новый экземпляра FileReader
                    var fileReader = new FileReader();
                    // Инициируем функцию FileReader
                    fileReader.onload = (function (file) {

                        return function (e) {
                            // Помещаем URI изображения в массив
                            dataArray.push({ name: file.name, value: this.result });
 
                            if ($('#filter').text().trim() === 'Текущая') addImage((dataArray.length - 1));
                        };

                    })(files[index]);
                     //Производим чтение картинки по URI
                    fileReader.readAsDataURL(file);
                }
            });
            return false;
        });
    }

    // Процедура добавления эскизов на страницу
    window.addImage=function addImage(ind) {
        // Если индекс отрицательный значит выводим весь массив изображений
        if (ind < 0) {
            start = 0; end = dataArray.length;
        } else {
            // иначе только определенное изображение 
            start = ind; end = ind + 1;
        }
        // Оповещения о загруженных файлах
        if (dataArray.length == 0) {
            // Если пустой массив скрываем кнопки и всю область
            $('#upload-button').hide();
            //$('#uploaded-holder').hide();
        } else if (dataArray.length == 1) {
            $('#upload-button span').html("Был выбран 1 файл");
        } else {
            $('#upload-button span').html(dataArray.length + " файлов были выбраны");
        }
        // Цикл для каждого элемента массива
        for (i = start; i < end; i++) {
            // размещаем загруженные изображения
            /*if ($('#dropped-files > .image').length <= maxFiles)*/ {
                $('#dropped-files').append('<li class="image">'
                        +'<div class="menu-pictures" id=img-' + i + '>'
                            +'<div class="btn-group">'
                                + '<button type="button" id="addpict' + (+$('input[name="picture_name"]').attr('value') + i + 1) + '" class="btn btn-default btn-sm addpict"><span class="glyphicon glyphicon-plus" style="color:blue"></span></button>'
                                + '<button type="button" id="fullpict' + (+$('input[name="picture_name"]').attr('value') + i + 1) + '" class="btn btn-default btn-sm fullpict"><span class="glyphicon glyphicon-fullscreen"></span></button>'
                                +'<a href="#" id="drop-' + i + '" class="btn btn-default btn-sm"><span class="glyphicon glyphicon-minus" style="color:red"></span></a>'
                            +'</div>'
                            + '<img src="'+dataArray[i].value+'" class="img-responsive" />'
                            + '<p> &lt;img='+(+ $('input[name="picture_name"]').attr('value')+i +1 )+'.jpg&gt; </p>'
                        +'</div>'
                    +'</li>');
                    //('<div id="img-' + i + '" class="image" style="background: url(' + dataArray[i].value + '); background-size: cover;"> <a href="#" id="drop-' + i + '" class="drop-button">Удалить изображение</a></div>');
            }
        }
        return false;
    }

    // Удаление только выбранного изображения 
    
    // Функция удаления всех изображений
    function restartFiles() {

        /*// Установим бар загрузки в значение по умолчанию
        $('#loading-bar .loading-color').css({ 'width': '0%' });
        $('#loading').css({ 'display': 'none' });
        $('#loading-content').html(' ');

        // Удаляем все изображения на странице и скрываем кнопки
        $('#upload-button').hide();
        $('#dropped-files > .image').remove();
        $('#uploaded-holder').hide();

        // Очищаем массив
        dataArray.length = 0;*/

        return false;
    }

    $('#dropped-files #upload-button .delete').click(restartFiles);

    // Загрузка изображений на сервер
    /*$('#upload-button .upload').on('click', function () { alert($('#topic').val()) })*/
    function  upload() {

        // Показываем прогресс бар
        $("#loading").show();
        // переменные для работы прогресс бара
        var totalPercent = 100 / dataArray.length;
        var x = 0;

        $('#loading-content').html('Загружен ');
        // Для каждого файла
        data.append("topic", $('#topic').val());
        $.ajax({
            type: "POST",
            url: getUrl(),
            contentType: false,
            processData: false,
            data: data,
            success: false,
            error:false
            //error: function (xhr, status, p3) {
            //    alert(xhr.responseText);
        });
        alert("result2:");
        /*var str = [];
        $.each(dataArray, function (index, file) {           
            str.push(dataArray[index].value);
        });
        $.post('/news/pictures', { images: str, topic: $('#topic').val() }, function (data) {
        });*/
        // Показываем список загруженных файлов
        $('#uploaded-files').show();
        return false;
    }/*);*/

    $('#ajax').on('drop', '#drop-files', function (e) {
        $(this).parent()
        event.preventDefault();
        
        // Передаем в files все полученные изображения
        var files = e.dataTransfer.files;
        // Проверяем на максимальное количество файлов
        loadInView(files);
        if ($('#filter').text().trim() != 'Текущая') $('#none').click();
    });

    $("#dropped-files").on("click", "a[id^='drop']", function (e) {
        // Предотвращаем стандартное поведение
        e.preventDefault();
        // получаем название id
        var elid = $(this).attr('id');
        // создаем массив для разделенных строк
        var temp = new Array();
        // делим строку id на 2 части
        temp = elid.split('-');
        // получаем значение после тире тоесть индекс изображения в массиве
        dataArray.splice(temp[1], 1);
        // Удаляем старые эскизы
        $('#dropped-files > .image').remove();
        // Обновляем эскизи в соответсвии с обновленным массивом
        addImage(-1);
    });

    /*for(var i=0;i< +$('input[name="picture_name"]').attr('value')+dataArray.length;i++)
    {*/
        $("#ajax").on('click', '.addpict', function () {
            //var j = i;
            var ta = $('#textarea'),
                p = ta[0].selectionStart,
                text = ta.val();
            if (p != undefined) {               
                ta.val(text.slice(0, p).trimRight() + ((p > 0) ? '\n' : '') + '<img=' + $(this).attr('id').replace("addpict", "") + '.jpg>\n' + text.slice(p).trimLeft());
                ta.trigger('focus');
                //ta[0].setSelectionRange(1, 1);
                ta[0].setSelectionRange(((text.slice(0, p).trimRight() + ((p > 0) ? '\n' : '') + '<img=' + $(this).attr('id').replace("addpict", "") + '.jpg>\n').length),((text.slice(0, p).trimRight() + ((p > 0) ? '\n' : '') + '<img=' + $(this).attr('id').replace("addpict", "") + '.jpg>\n').length));
                //range.moveStart('character', (text.slice(0, p).trimRight() + ((p > 0) ? '\n' : '') + '<image=' + $(this).attr('id').replace("addpict", "") + '.jpg>\n').length);
            }

            else {
                ta.trigger('focus');
                range = document.selection.createRange();
                range.text = '<img=' + $(this).attr('id').replace("addpict", "") + '.jpg>';
            }
            //alert(/*"i: " + i + " j:  " + j +*/ " p: " + p + " ta.val(): " + ta.val())
        });
        //bb код - <b>
        $("#b").on('click', function (e) {
            e.preventDefault();
            var ta = $('#textarea'),
                st = ta[0].selectionStart,
                en = ta[0].selectionEnd,
                text = ta.val();
            ta.val(text.slice(0, st) + '<b>' + text.slice(st, en) + '</b>' + text.slice(en));
            ta.trigger('focus');
            ta[0].setSelectionRange(st, en+7);
        });
        //bb код - <i>
        $("#i").on('click', function (e) {
            e.preventDefault();
            var ta = $('#textarea'),
                st = ta[0].selectionStart,
                en = ta[0].selectionEnd,
                text = ta.val();
            ta.val(text.slice(0, st) + '<i>' + text.slice(st, en) + '</i>' + text.slice(en));
            ta.trigger('focus');
            ta[0].setSelectionRange(st, en + 7);
        });
        //bb код - <blockquote>
        $("#quote").on('click', function (e) {
            e.preventDefault();
            var ta = $('#textarea'),
                st = ta[0].selectionStart,
                en = ta[0].selectionEnd,
                text = ta.val();
            ta.val(text.slice(0, st) + '<blockquote>' + text.slice(st, en) + '</blockquote>' + text.slice(en));
            ta.trigger('focus');
            ta[0].setSelectionRange(st, en + 25);
        });
        $("#ajax").on('click', '.fullpict', function () {
            /*alert($(this).attr('id').replace("fullpict", ""));
            alert(+$('input[name = "picture_name"]').val());
            alert(+$(this).attr('id').replace("fullpict", "") - +$('input[name = "picture_name"]').val());*/
            //alert($('#filter').text());

            //alert(dataArray[(+$(this).attr('id').replace("fullpict", "") - +$('input[name = "picture_name"]').val()) - 1]);
            if ($('#filter').text().trim() === 'Текущая') $('#modal img').attr('src', dataArray[(+$(this).attr('id').replace("fullpict", "") - +$('input[name = "picture_name"]').val()) - 1].value);
            else $('#modal img').attr('src', '/Content/img/' + $(this).attr('id').replace("fullpict", "") + '.jpg');
            $('#modal').modal('show');

            //$("body").append("<div id='image-fullscreen'><img src='" + dataArray[(+$(this).attr('id').replace("fullpict", "") - +$('input[name = "picture_name"]').val()) - 1].value + "' class='img-responsive center-block' </div>");
        });
        $("#fake-submit").on('click', function (e) {
            upload();
            $("#submit").click();
        });
        $("#frm").on('submit', function(e)
        {
            e.preventDefault;
            return false;
        });

});


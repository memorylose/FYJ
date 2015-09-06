$(document).ready(function () {
    $('#submitForm')
        .bootstrapValidator({
            message: 'This value is not valid',
            feedbackIcons: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            fields: {
                email: {
                    validators: {
                        notEmpty: {
                            message: '邮件地址不能为空'
                        },
                        remote: {
                            type: 'POST',
                            url: '../AjaxHandler/UserHandler.ashx?Method=CheckUserMail',
                            data: function (validator) {
                                return {
                                    userMail: validator.getFieldElements('email').val()
                                };
                            },
                            message: '用户名已存在',
                        },
                        emailAddress: {
                            message: '邮件格式错误'
                        }
                    }
                },
                password: {
                    validators: {
                        notEmpty: {
                            message: '密码不能为空'
                        },
                        stringLength: {
                            min: 6,
                            max: 30,
                            message: '密码最短为6位'
                        },
                        identical: {
                            field: 'confirmPassword',
                            message: '两次密码不一致'
                        }
                    }
                },
                confirmPassword: {
                    validators: {
                        notEmpty: {
                            message: '重复密码不能为空'
                        },
                        stringLength: {
                            min: 6,
                            max: 30,
                            message: '密码最短为6位'
                        },
                        identical: {
                            field: 'password',
                            message: '两次密码不一致'
                        }
                    }
                },
                code: {
                    validators: {
                        callback: {
                            message: '验证码错误',
                            callback: function (value, validator) {
                                return value == getCookie('CheckCode').toLocaleLowerCase();
                            }
                        }
                    }
                }
            }
        })
        .on('success.form.bv', function (e) {
            e.preventDefault();
            var $form = $(e.target);
            var bv = $form.data('bootstrapValidator');
            $.post($form.attr('action'), $form.serialize(), function (result) {
                for (var i in result) {
                    if (result[i] == 'failed') {
                        $('#errorMsg').css('display', 'block');
                        $('#errorMsg').text('验证信息有误，请重试');
                    }
                    else if (result[i] == 'success') {
                        location.href = '/Article/Index';
                    }
                }
            }, 'json');
        });
});
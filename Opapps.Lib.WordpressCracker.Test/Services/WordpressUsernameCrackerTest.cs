﻿using Opapps.Lib.WordpressCracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Test.Services
{
    public class WordpressUsernameCrackerTest
    {
        private readonly WordpressUsernameCracker _service;
        private readonly static Uri TESTING_URL = new Uri("http://testing1.test/wp-login.php?wp_lang=en_US");
        public WordpressUsernameCrackerTest()
        {
            _service = new();
        }

        [Fact]
        public async Task AttemptGettingValidUsername_WhenUsernameIsInvalid_ReturnFalse()
        {
            bool res = await _service.AttemptGettingValidUsername(TESTING_URL, "salah");
            Assert.False(res);
        } 
        
        [Fact]
        public async Task AttemptGettingValidUsername_WhenUsernameIsValid_ReturnTrue()
        {
            bool res = await _service.AttemptGettingValidUsername(TESTING_URL, "ihsan");
            Assert.True(res);
        }
    }
}
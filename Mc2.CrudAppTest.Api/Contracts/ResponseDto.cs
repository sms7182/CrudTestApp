using System;
using System.Collections;
using System.Collections.Generic;

namespace Mc2.CrudAppTest.Api.Contracts
{
    public class ResponseDto
    {

        public ResponseDto()
        {
            Errors = new List<string>();
    

        }
        public Guid? Id { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
        public IList? Data { get; set; }
     
        
    
    }

    
}

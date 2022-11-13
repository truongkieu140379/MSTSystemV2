using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Braintree;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorSearchSystem.BraintreeHelpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TutorSearchSystem.Controllers
{
    [Authorize]
    [Route("api/braintree-payment")]
    [ApiController]
    public class BrainTreeController : ControllerBase
    {
        BraintreeGateway gateway;

        public BrainTreeController()
        {
            gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = "cb3s242yw7g4yj73",
                PublicKey = "vtx63pfv376kbvzh",
                PrivateKey = "7682f8be02aa68b0a9a446a258d47500"
            };
        }


        // GET: api/<BrainTreeController>
        [HttpGet]
        public string Get()
        {
            //return client token
            return  gateway.ClientToken.Generate();
        }

        

        // POST api/<BrainTreeController>
        [HttpPost]
        public IActionResult Post([FromBody] BraintreeModel value)
        {
            var nonce = value.nonce;
            var amount = value.amount;
            var request = new TransactionRequest
            {
                Amount = Convert.ToDecimal(amount),
                PaymentMethodNonce = nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true,
                }
            };
            var result = gateway.Transaction.Sale(request);
            if(result.Target.ProcessorResponseText.Equals("Approved"))
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        
    }
}

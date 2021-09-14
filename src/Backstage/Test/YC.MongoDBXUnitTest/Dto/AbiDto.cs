using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.MongoDbXUnitTest
{

    public class InputsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }
    }

    public class OutputsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }
    }

    public class ItemDto
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Constant { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<InputsItem> Inputs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<OutputsItem> Outputs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Payable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StateMutability { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }
    }


    public class AbiDto
    {

        public AbiDto()
        {
            this.ItemDtos = new List<ItemDto>();
        }
        public List<ItemDto> ItemDtos { get; set; }
    }
}

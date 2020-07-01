using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace archi_Template
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        public static string rs;
        public static string rt;
        public static int[] reg_arr = new int[27];
        public static Hashtable data_mem = new Hashtable();
        private void UserCodetxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void MipsRegisterGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RunCycleBtn_Click(object sender, EventArgs e)
        {
          
            
            int pc_add= int.Parse(" StartPCTxt.Text");
            
            string instructions = UserCodetxt.Text;
            string IF_ID = pc_add.ToString() + ":" + instructions;
            foreach (DictionaryEntry s in data_mem)
            {
                MemoryGrid.Rows.Add(s.Key, s.Value);
            }


            Hashtable inst_mem = new  Hashtable();

            void instructionMemory(string pc_counter,string instruction)
            {
                inst_mem.Add(pc_counter, instruction);
            }

            int reg_dest =0, alu_op1 = 0, alu_op0 =0,
                  alusrc = 0, mem_read = 0, mem_write = 0, branch =0,
                 reg_write = 0, mem_to_reg = 0;

            string control_unit( string oppo_code)
            {
                
                if (oppo_code == "000000")
                {

                    reg_dest = 1;
                    alusrc = 0;
                    alu_op1 = 1;
                    alu_op0 = 0;
                    mem_to_reg = 0;
                    reg_write = 1;
                    mem_read = 0;
                    mem_write = 0;
                    branch = 0;
                }

                if (oppo_code == "100011")
                {
                    reg_dest = 0;
                    alusrc = 1;
                    alu_op1 = 0;
                    alu_op0 = 0;
                    mem_to_reg = 1;
                    reg_write = 1;
                    mem_read = 1;
                    mem_write = 0;
                    branch = 0;
                }
            
            string ex = (reg_dest + alu_op1 + alu_op1 + alusrc).ToString();
            string mem = (mem_read + mem_write + branch).ToString();
            string wb = (reg_write + mem_to_reg).ToString();
            return ex + "," + mem + "," + wb;
        }


        void reg_file(string read_reg1, string read_reg2,string write_reg,string write_data, int reg_writ)
        {
            if (reg_writ == 0)
            {
                rs = read_reg1;
                rt = read_reg2;
            }
           else if (reg_writ == 1)
                {
                    data_mem.Add(write_reg, write_data);
                }  
        }

            string data_memory(string address, string write_data, int memWrite, int memRead)
        {
            if (memRead == 1)
             data_mem.ContainsKey(address);
            else if(memWrite == 1)
             data_mem.Add(address, write_data);
                string data = write_data;

                return data;
        }

            int Alu(string data1, string data2, int op1, int op0, string func_code)
        {
                if (op1 == 0 && op0 == 0)
                {
                    return int.Parse (data1 + data2);
                }
                else if (op1 == 1 && op0 == 0)
                {
                    if (func_code == "100000")
                    {
                        return int.Parse(data1 + data2);

                    }
                    else if (func_code == "100010")
                    {
                        return int.Parse(data1 + data2);
                    }
                    else if (func_code == "100100")

                    {
                        return int.Parse(data1 + data2);
                    }
                    else if (func_code == "100101")
                    {
                        return int.Parse(data1 + data2);
                    }
                    
                }
        }


        string sign_extend(string v, string v1, string address)
        {
          int address2=Int32.Parse("address");
               return address2.ToString();
               
        }
        string Mux(string select1, string select2, int selector)
        {
            if (selector == 0)
                return select1;
           else if (selector == 1)
                return select2;
        }
            string spread(string par)
            {
                string opcode = par.Substring(0, 6);
                string rs = par.Substring(6, 5);
                string rt = par.Substring(11, 5);
                string rd = par.Substring(16, 5);
                string shamt = par.Substring(21, 5);
                string funct = par.Substring(26, 6);

                return instructions = opcode + " " + rs + " " + rt + " " + rd + " " + shamt + " " + funct;
            }


            void fetch(int pc)
            {
                pc += 4;
               instructionMemory(pc.ToString(), instructions);

            }




            string decode()
            {
             string[] parts = IF_ID.Split(':');
             string instruction = spread(parts[1]);
             string[] instructionParts = instruction.Split(' ');
             string Lines = control_unit(instructionParts[0]);
                string d12 =null;
                string extended = null;
             if (instructionParts[0] == "000000")
             {
                d12  = reg_file(instructionParts[1], instructionParts[2], " ", " ", 0);

                 extended = sign_extend(instructionParts[3], instructionParts[4], instructionParts[5]);
             }
             else if(instructionParts[0] == "100011")
             {
                 d12 = reg_file(instructionParts[1], instructionParts[2], "", "", 0);
                 extended = sign_extend(instructionParts[3], instructionParts[4], instructionParts[5]);
             }
            
              string ID_EX = d12 + " " + extended + " " + instructionParts[2] + " " + instructionParts[3] + " " + Lines;
                return ID_EX;
             }
            
         string Execute()
            {
                string[] parts = IF_ID.Split(':');
                string instruction = spread(parts[1]);
                string[] instructionParts = instruction.Split(' ');
                string Lines = control_unit(instructionParts[0]);
                string sel2 = instructionParts[3] + instructionParts[4] + instructionParts[5];
                int exec = 0;
                string sel ,dest=null;
        

                if (instructionParts[0] == "000000")
                {
                    sel = Mux(instructionParts[2],sel2 , 0);
                    exec = Alu(instructionParts[1],sel,1,0,instructionParts[5]) ;
                    dest = Mux(instructionParts[2], instructionParts[3], 1);
                }
                else if (instructionParts[0] == "100011")
                {
                    sel = Mux(instructionParts[2], sel2, 1);
                    exec  = Alu(instructionParts[1], sel, 1, 0, instructionParts[5]);
                    dest = Mux(instructionParts[2], instructionParts[3], 1);
                }
                string EX_MEM = exec + " " + dest + " " + instructionParts[2]+" "+instruction[0];
                return EX_MEM;

            }
            string Mem_access()
            {
                string data = Execute();
                string[] dataparts = data.Split(' ');
                string mem = data_memory(dataparts[0], dataparts[2], int.Parse(control_unit(dataparts[3])),int.Parse(control_unit(dataparts[3])));
                string MEM_WB = dataparts[0] + " " + dataparts[1] + " " + mem;
                return MEM_WB;

            }

            string write_back ()
            {
                string wr = Mem_access();
                string[] wrparts = wr.Split();
                string w = Mux(wrparts[0], wrparts[2], 0);
                string WB = w + " " + wrparts[1];
                reg_file("", "", wrparts[0], wrparts[1], 1);
                return WB;
            }



        }

        private void StartPCTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void InializeBtn_Click(object sender, EventArgs e)
        {
             
            for (int i = 0; i < 27; i++)
            {
                if (i > 0)
                    reg_arr[i] = i + 100;
                else
                    reg_arr[0] = 0;

            }

           
            for (int i = 0; i < 128; i++)
            {

                data_mem.Add(reg_arr[i], 99);
            }
        }

        private void MemoryGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

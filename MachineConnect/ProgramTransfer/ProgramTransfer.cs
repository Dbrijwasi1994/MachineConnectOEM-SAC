using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using DTO;
using FocasLibrary;
namespace CNC_PT
{
    public static partial class ProgramTransferPT
    {
        //cnc_rdprogdir3
        public static List<ProgramDTO> ReadAllPrograms(string ipAddress, ushort portNo,short programDetailType, string folderPath,bool isProgramFolderSupports) // ProgramDetailype=2
        {
            List<ProgramDTO> programs = new List<ProgramDTO>();   
           // for testing TODO - comment below four lines
            //programs.Add(new ProgramDTO() { ProgramNo = 123, Comment = "testing1", ModifiedDate = DateTime.Now, ProgramLenght = 100 });
            //programs.Add(new ProgramDTO() { ProgramNo = 124, Comment = "testing2", ModifiedDate = DateTime.Now, ProgramLenght = 101 });
            //programs.Add(new ProgramDTO() { ProgramNo = 125, Comment = "testing3", ModifiedDate = DateTime.Now, ProgramLenght = 102 });
            //return programs;
            int topProgram = 0;
            short prgromsToRead = 10;
            short ret = 0;
            ushort focasLibHandle = 0;
            
            ret = FocasLib.cnc_allclibhndl3(ipAddress, portNo, 10, out focasLibHandle);
            if (ret != 0)
            {
                Logger.WriteErrorLog("cnc_allclibhndl3() failed. return value is = " + ret);
                MessageBox.Show("Not able to connect to CNC machine. Please check the network connection and try again");
                return null;
            }
            if (isProgramFolderSupports == false)
            {
                ret = 0;
                while (prgromsToRead >= 10)
                {
                    FocasLibBase.PRGDIR3 d = new FocasLibBase.PRGDIR3();
                    try
                    {
                        ret = FocasLib.cnc_rdprogdir3(focasLibHandle, programDetailType, ref topProgram, ref prgromsToRead, d);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteErrorLog(ex.ToString());
                    }
                    if (ret != 0)
                    {
                        Logger.WriteErrorLog("cnc_rdprogdir3() failed. return value is = " + ret);
                        MessageBox.Show("cnc_rdprogdir3() failed. return value is = " + ret);
                        break;
                    }
                    if (prgromsToRead == 0)
                    {
                        Logger.WriteErrorLog("No more programss to read/found.");
                        break;
                    }

                    if (prgromsToRead >= 1) get_program(programs, d.dir1);
                    if (prgromsToRead >= 2) get_program(programs, d.dir2);
                    if (prgromsToRead >= 3) get_program(programs, d.dir3);
                    if (prgromsToRead >= 4) get_program(programs, d.dir4);
                    if (prgromsToRead >= 5) get_program(programs, d.dir5);
                    if (prgromsToRead >= 6) get_program(programs, d.dir6);
                    if (prgromsToRead >= 7) get_program(programs, d.dir7);
                    if (prgromsToRead >= 8) get_program(programs, d.dir8);
                    if (prgromsToRead >= 9) get_program(programs, d.dir9);
                    if (prgromsToRead >= 10) get_program(programs, d.dir10);

                    var lastProgramNo = programs[programs.Count - 1].ProgramNo;
                    if (Int32.TryParse(lastProgramNo, out topProgram))
                    {
                        topProgram++;
                    }

                }
            }
            else
            {
                FocasLibBase.ODBPDFNFIL NoOfSubFolder = new FocasLibBase.ODBPDFNFIL();                
                ret = FocasLib.cnc_rdpdf_subdirn(focasLibHandle, folderPath, NoOfSubFolder);
                short num_prog = 1;                             
                if (NoOfSubFolder.file_num > 0)
                {
                    for (short i = 0; i < NoOfSubFolder.file_num; i++)
                    {
                        //cnc_rdpdf_alldir : Reads the file / folder information under the specified folder.                       
                        FocasLibBase.IDBPDFADIR pdf_adir_in = new FocasLibBase.IDBPDFADIR();
                        pdf_adir_in.path = folderPath;
                        pdf_adir_in.req_num = i;
                        pdf_adir_in.type = 0;
                        pdf_adir_in.size_kind = 1;

                        num_prog = 1;
                        FocasLibBase.ODBPDFADIR1 pdf_adir_out1 = new FocasLibBase.ODBPDFADIR1();
                        ret = FocasLib.cnc_rdpdf_alldir(focasLibHandle, ref num_prog, pdf_adir_in, pdf_adir_out1);
                        if (pdf_adir_out1.info1.data_kind == 1) get_programOITF(programs, pdf_adir_out1.info1);

                    }
                }
            }
            FocasLib.cnc_freelibhndl(focasLibHandle);
            return programs;
        }

        private static void get_program(List<ProgramDTO> ls, FocasLibrary.FocasLibBase.PRGDIR3_data dir)
        {
            ProgramDTO pDto = new ProgramDTO();
            pDto.ProgramNo = dir.number.ToString();
            pDto.ProgramLenght = dir.length;
            pDto.Comment = dir.comment;
            pDto.IsSupportFolder = false;
            try
            {
                pDto.ModifiedDate = new DateTime(dir.mdate.year < 2000 ? dir.mdate.year + 2000 : dir.mdate.year, dir.mdate.month, dir.mdate.day, dir.mdate.hour, dir.mdate.minute, 00);
                ls.Add(pDto);
            }
            catch { }
           
        }
        private static void get_programOITF(List<ProgramDTO> ls, FocasLibrary.FocasLibBase.ODBPDFADIR dir)
        {
            ProgramDTO pDto = new ProgramDTO();
            pDto.ProgramNo = dir.d_f.Trim();
            pDto.ProgramLenght = 0;
            pDto.Comment = dir.comment;
            try
            {
                pDto.ModifiedDate = new DateTime(dir.year < 2000 ? dir.year + 2000 : dir.year, dir.mon, dir.day, dir.hour, dir.min, 00);
                ls.Add(pDto);
            }
            catch { }

        }


        //to download use below functions
        //cnc_upstart3,cnc_upload3, cnc_upend3
        public static string DownloadProgram(string ipAddress, ushort portNo, string file, string destFolder, bool compareProgram, string comments, bool displayMessage, string folderPath, bool isProgramFolderSupports)
        {
            int programNo = 0;
            string programNoStr = "";
            if (isProgramFolderSupports == false)
            {
               programNo = Convert.ToInt32(file.Replace(" ", "").TrimStart(new char[]{'O'}));
            }
            else
            {
                programNoStr = file;
            }
            short ret = -20;
            int bufsize = 1280;
            int dataLength = 0;
            ushort focasLibHandle = 0;    
            string programDownloaded = string.Empty;
            ret = FocasLib.cnc_allclibhndl3(ipAddress, portNo, 10, out focasLibHandle);
            if (ret != 0)
            {
                Logger.WriteErrorLog("cnc_allclibhndl3() failed. return value is = " + ret);
                MessageBox.Show("Not able to connect to CNC machine. Please check the network connection and try again");
                return string.Empty;
            }
            int count = 0;
            ret = short.MinValue;          

            while (ret != 0 && count < 10)
            {
                if (isProgramFolderSupports)
                {
                    //ret = FocasLib.cnc_upstart4(focasLibHandle, 0, programNoStr);
                    ret = FocasLib.cnc_upstart4(focasLibHandle, 0, folderPath + programNoStr);
                }
                else
                {
                    ret = FocasLib.cnc_upstart3(focasLibHandle, 0, programNo, programNo);
                }                
                
                count++;
                if (ret == -1) Thread.Sleep(200);
            }
            if (ret == -1)
            {
                MessageBox.Show("CNC is busy. Please try after some time.");
                FocasLib.cnc_freelibhndl(focasLibHandle);
                return "";
            }
           
            if (ret != 0)
            {
                Logger.WriteErrorLog("cnc_upstart3/4() failed. return value is = " + ret);
                FocasLib.cnc_freelibhndl(focasLibHandle);
                MessageBox.Show("cnc_upstart3/4() failed. return value is = " + ret);
                return string.Empty;
            }
            StringBuilder programStr = new StringBuilder(bufsize*10);           
            bool endFound = false;
            do
            {
                char[] buf = new char[bufsize + 1];
                dataLength = bufsize;
                if (isProgramFolderSupports)
                {
                    ret = FocasLib.cnc_upload4(focasLibHandle, ref dataLength, buf);
                }
                else
                {
                    ret = FocasLib.cnc_upload3(focasLibHandle, ref dataLength, buf);
                }

                if (ret == 10) // if buffer is empty retry
                {
                    Thread.Sleep(400); 
                    continue;
                   
                }
                if (ret == -2) //if buffer is in reset mode Write protected on CNC side
                {
                    Logger.WriteErrorLog("cnc_upload3() failed. return value is = " + ret);
                    MessageBox.Show("cnc_upload3() failed. return value is = " + ret);
                    break;
                }
                if (ret == 7) //if buffer is in reset mode Write protected on CNC side
                {
                    Logger.WriteErrorLog("cnc_upload3() failed. return value is = " + ret);
                    MessageBox.Show("Write protected on CNC side. return value from cnc_upload3() is = " + ret);
                    break;
                }
                if (ret != 0)
                {
                    Logger.WriteErrorLog("cnc_upload3() failed. return value is = " + ret);
                    MessageBox.Show("cnc_upload3() failed. return value is = " + ret);
                    break;
                }

                char[] tempBuf = new char[dataLength];
                Array.Copy(buf, tempBuf, dataLength);
                programStr.Append(tempBuf);
                if (buf[dataLength - 1] == '%')
                {
                    endFound = true;
                    break;
                   
                }
                Thread.Sleep(600);

            } while (endFound == false);
            if (ret == 7)
            {
                FocasLib.cnc_freelibhndl(focasLibHandle);
                return string.Empty;
            }
            if (isProgramFolderSupports)
            {
                ret = FocasLib.cnc_upend4(focasLibHandle);
            }
            else
            {
                ret = FocasLib.cnc_upend3(focasLibHandle);
            }
            
            if (ret != 0)
            {
                Logger.WriteErrorLog("cnc_upend34() failed. return value is = " + ret);
                MessageBox.Show("cnc_upend3() failed. return value is = " + ret);
                return string.Empty;
            }
            if (string.IsNullOrEmpty(Convert.ToString(programStr)))
            {
                Logger.WriteDebugLog("program is empty.");
                MessageBox.Show("program is empty.");
                return string.Empty;
            }
            programStr.Replace("\r", "").Replace("\n", "\r\n");
            if (compareProgram)
            {
                string tempFile = string.Format("{0}{1}.txt",isProgramFolderSupports ? programNoStr: "O" + programNo.ToString(),SafeFileName(comments));
                tempFile = Path.Combine("Temp", tempFile);
                if(!Directory.Exists(Path.Combine(destFolder, "temp")))
                {
                    try
                    {
                        Directory.CreateDirectory(Path.Combine(destFolder, "temp"));
                    }
                    catch { }
                }
                tempFile = Path.Combine(destFolder, tempFile);
                File.WriteAllText(tempFile, programStr.ToString());
                return tempFile;
            }
            else
            {
                //file = file + SettingsPT.SafeFileName(comments);
                file = string.Format("{0}{1}", isProgramFolderSupports ? programNoStr : "O" + programNo.ToString(), SafeFileName(comments));
                string version = GetVersion(destFolder, file, programStr);
                if (version.ToLower().IndexOf("exists") >= 0)
                {
                    Logger.WriteDebugLog(version);
                    MessageBox.Show(version);
                    version = string.Empty;
                    return string.Empty;
                }
                if (version.ToLower().IndexOf("ERROR") >= 0)
                {
                    Logger.WriteDebugLog("Code Error");
                    MessageBox.Show(version);
                    return string.Empty;
                }
                try
                {
                    File.WriteAllText( version, programStr.ToString());
                }
                catch (Exception ex)
                {
                    Logger.WriteDebugLog(ex.ToString());
                    MessageBox.Show(ex.Message);
                }

                if (File.Exists(version))
                {                    
                    programDownloaded = Path.GetFileNameWithoutExtension(version);
                    Logger.WriteDebugLog(string.Format("Program {0} saved successfully.",programDownloaded ));
                    if(displayMessage)
                    MessageBox.Show(string.Format("Program {0} saved successfully.", programDownloaded));
                }
            }
            FocasLib.cnc_freelibhndl(focasLibHandle);
            return programDownloaded;
        }

        private static string GetVersion(string folder, string file,StringBuilder program)//returns complete filepath(with version)//
        {
            bool matching = false;
            int i = 1;
            file = file + ".txt";

            if (!File.Exists( Path.Combine(folder , file)))//if 1000 not exists??
            {
                return Path.Combine(folder ,file);

            }
            else
            {
                matching = CompareContents(Path.Combine(folder, file), program);
                if (matching == true)
                {
                    return "Same version of program exists on the computer system as " + Path.GetFileNameWithoutExtension(Path.Combine(folder, file));
                }

                string pwithext = "";
                string path_wtout_exten = Path.GetFileNameWithoutExtension(Path.Combine(folder, file));//
                while (true)
                {
                    pwithext = Path.Combine( folder ,  path_wtout_exten + "." + i.ToString() + ".txt");
                    if (!File.Exists(pwithext))//here we can comapre with folder+file contents also//
                    {
                        return pwithext;
                    }
                    else//if file exists
                    {

                        matching = CompareContents(pwithext, program);//compare with downloaded file//
                        if (matching == true)
                        {
                            return "Same version of program exists on the computer system as " + Path.GetFileNameWithoutExtension(pwithext);

                        }
                        i++;
                        if (i >= 1000)
                            return "ERROR";
                    }
                }
            }


        }

        private static bool CompareContents(string pwithext, StringBuilder programRecieved)
        {
            //TODO
            string str1 = File.ReadAllText(pwithext);
            if (str1.Equals(programRecieved.ToString(),StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }


        //to sent to cnc
        //cnc_dwnstart3, cnc_download3, cnc_dwnend3
        public static bool UploadProgram(string ipAddress, ushort portNo, string program, string folderPath, bool isProgramFolderSupports )
        {
            short ret = -20;
            int len, n;
            ushort focasLibHandle = 0;
            ret = FocasLib.cnc_allclibhndl3(ipAddress, portNo, 10, out focasLibHandle);
            if (ret != 0)
            {
                Logger.WriteErrorLog("cnc_allclibhndl3() failed. return value is = " + ret);
                MessageBox.Show("Not able to connect to CNC machine. Please check the network connection and try again");
                return false;
            }          

            int count = 0;
            ret = short.MinValue;
            while (ret != 0 && count < 20)
            {
                if (isProgramFolderSupports == false)
                {
                    ret = FocasLib.cnc_dwnstart3(focasLibHandle, 0);
                }
                else
                {
                    ret = FocasLib.cnc_dwnstart4(focasLibHandle, 0, folderPath);
                }
                count++;
                if (ret == -1)
                {
                    Thread.Sleep(200);
                }                
            }
            if (ret == -1)
            {
                if (isProgramFolderSupports == false)
                {
                    FocasLib.cnc_dwnend(focasLibHandle);
                }
                else
                {
                    FocasLib.cnc_dwnend4(focasLibHandle);
                }
                FocasLib.cnc_freelibhndl(focasLibHandle);
                MessageBox.Show("CNC is busy. Please try after some time.");                
                return false;
            }
            if (ret != 0)
            {
                Logger.WriteErrorLog("cnc_dwnstart3() failed. return value is = " + ret);
                FocasLib.cnc_freelibhndl(focasLibHandle);
                MessageBox.Show("cnc_dwnstart3() failed. return value is = " + ret);
                return false;
            }
            if (ret == FocasLib.EW_OK)
            {
                len = program.Length;
                while (len > 0)
                {
                    n = program.Length;
                    if (isProgramFolderSupports == false)
                    {
                        ret = FocasLib.cnc_download3(focasLibHandle, ref n, program);
                    }
                    else
                    {
                        ret = FocasLib.cnc_download4(focasLibHandle, ref n, program);
                    }
                   
                    if (ret == 10) //if buffer is empty
                    {
                        continue;
                    }
                    if (ret == -2) // if buffer in reset mode
                    {
                        break;
                    }
                    if (ret != FocasLib.EW_OK)
                    {
                        Logger.WriteErrorLog("cnc_download3() failed. return value is = " + ret);
                        MessageBox.Show("cnc_download3() failed. return value is = " + ret);
                        break;
                    }
                   
                    if (ret == FocasLib.EW_OK)
                    {
                        //program += n;
                        //len -= n;

                        program = program.Substring(n,len-n);
                        len -= n;
                    } 
                }
                if (isProgramFolderSupports == false)
                {
                    ret = FocasLib.cnc_dwnend(focasLibHandle);
                }
                else
                {
                    ret = FocasLib.cnc_dwnend4(focasLibHandle);
                }
                if (ret != FocasLib.EW_OK)
                {
                    Logger.WriteErrorLog("cnc_dwnend() failed. return value is = " + ret);
                    FocasLib.cnc_freelibhndl(focasLibHandle);
                    if (ret != 7) MessageBox.Show("cnc_dwnend() failed. return value is = " + ret);
                    if (ret == 7)
                    {
                        MessageBox.Show("Upload program failed because write protected on CNC side.");
                    }
                    return false;
                }
                FocasLib.cnc_freelibhndl(focasLibHandle);        
            }
            return true;      
        }
     
        //to search        
        /*
          The behavior of this function depends on the CNC mode. 
          EDIT, MEM mode : foreground search 
          Other mode : background search 
         */
        public static bool SearchPrograms(string ipAddress, ushort portNo,short programNumber)
        {
            bool isFound = false;
            focas_ret ret;
            int ret1 = -1;
            ushort focasLibHandle = 0;
            ret1 = FocasLib.cnc_allclibhndl3(ipAddress, portNo, 10, out focasLibHandle);
            if (ret1 != 0)
            {
                Logger.WriteErrorLog("cnc_allclibhndl3() failed. return value is = " + ret1);
                MessageBox.Show("Not able to connect to CNC machine. Please check the network connection and try again");                
                return false;
            }
            short retShort = FocasLib.cnc_search(focasLibHandle, programNumber);
            Enum.TryParse<focas_ret>(retShort.ToString(), out ret);
            switch (ret)
            {
                case focas_ret.EW_OK:
                    Logger.WriteDebugLog("PROGRAM " + programNumber + "  has been searched.");
                    isFound = true;
                    break;
                case focas_ret.EW_DATA:
                    Logger.WriteDebugLog("PROGRAM " + programNumber + " doesn't exist.");
                    break;
                case focas_ret.EW_PROT:
                    Logger.WriteDebugLog("Program " + programNumber + " is PROTECTED.");
                    break;
                case focas_ret.EW_BUSY:
                    Logger.WriteDebugLog("CNC is busy. Program " + programNumber + " search REJECTED.");
                    break;                
            }
            FocasLib.cnc_freelibhndl(focasLibHandle);
            return isFound;
        }

        //to delete EW_PASSWD
        public static bool DeletePrograms(string ipAddress, ushort portNo, string programNumber,string folderPath,bool isProgramFolderSupports)
        {
            short tempProgramNo = 0;
            string tempProgramNoStr = "";
            if (isProgramFolderSupports == false)
            {
                short.TryParse(programNumber.TrimStart(new char[]{'O',' '}), out tempProgramNo);
                if (tempProgramNo == 0) return false;
            }
            else
            {
                tempProgramNoStr = programNumber;
            }
           
            bool isdeleted = false;
            focas_ret ret;
            int ret1 = -1;
            ushort focasLibHandle = 0;
            ret1 = FocasLib.cnc_allclibhndl3(ipAddress, portNo, 10, out focasLibHandle);
            if (ret1 != 0)
            {
                Logger.WriteErrorLog("cnc_allclibhndl3() failed. return value is = " + ret1);
                MessageBox.Show("Not able to connect to CNC machine. Please check the network connection and try again");
                return false;
            }

            if (isProgramFolderSupports == true)
            {
                string fileName = folderPath + programNumber;
                ret1 = FocasLib.cnc_pdf_del(focasLibHandle, fileName);
            }
            else
            {
                ret1 = FocasLib.cnc_delete(focasLibHandle, tempProgramNo);              
            }

            Enum.TryParse<focas_ret>(ret1.ToString(), out ret);

            
            switch (ret)
            {
                case focas_ret.EW_OK:
                    Logger.WriteDebugLog("PROGRAM " + programNumber + " has been deleted.");
                    MessageBox.Show("PROGRAM " + programNumber + " has been deleted. Please wait while updating the program list.");
                    isdeleted = true;
                    break;
                case focas_ret.EW_DATA:
                    Logger.WriteDebugLog("PROGRAM " + programNumber + " doesn't exist.");
                    MessageBox.Show("PROGRAM " + programNumber + " doesn't exist.");
                    break;
                case focas_ret.EW_PROT:
                    Logger.WriteDebugLog("Program " + programNumber + " is PROTECTED.");
                    MessageBox.Show("Program " + programNumber + " is PROTECTED.");
                    break;
                case focas_ret.EW_BUSY:
                    Logger.WriteDebugLog("CNC is busy. Program " + programNumber + "  delete REJECTED.");
                    MessageBox.Show("CNC is busy. Program " + programNumber + "  delete REJECTED.");
                    break;
                case focas_ret.EW_PASSWD:
                    Logger.WriteDebugLog("Specified program number cannot be deleted because the data is protected.");
                    MessageBox.Show("Specified program number cannot be deleted because the data is protected.");
                    break;
                case focas_ret.DEL_FAILED:
                    MessageBox.Show("CNC execution rejected. CNC is on execution or CNC is in the emergency stop. ");
                    break;
            }
            FocasLib.cnc_freelibhndl(focasLibHandle);
            return isdeleted;
        }

        //running program number
        //cnc_rdprgnum       
        public static short ReadRunningProgramNumber(string ipAddress,ushort portNo, out short mainProgram)
        {
            ushort focasLibHandle = 0;
            short ret = FocasLib.cnc_allclibhndl3(ipAddress, portNo, 10, out focasLibHandle);
            if (ret != 0)
            {
                Logger.WriteErrorLog("cnc_allclibhndl3() failed. return value is = " + ret);
                MessageBox.Show("Not able to connect to CNC machine. Please check the network connection and try again");
                return mainProgram = 0;
            }
            FocasLibBase.ODBPRO odbpro = new FocasLibBase.ODBPRO();
            FocasLib.cnc_rdprgnum(focasLibHandle, odbpro);
            mainProgram = odbpro.mdata;
            FocasLib.cnc_freelibhndl(focasLibHandle);
            return odbpro.data;
        }

        public static bool CheckProgramExistence(string programNo , List<ProgramDTO> ls)
        {
            int index=-1;
            bool status = false;
            if (ls != null && ls.Count > 0)
            {
                try
                {
                    index = ls.FindIndex(delegate(ProgramDTO pd1) { return (pd1.ProgramNo.TrimStart(new char[]{'O'}).Equals(programNo.TrimStart(new char[]{'O'}),StringComparison.OrdinalIgnoreCase)); });
                    if (index >= 0)
                    {
                        status = true;
                    }
                }
                catch (Exception ex)
                {
                    status = false;
                    Logger.WriteDebugLog(ex.ToString());
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                status = false;
            }
            return status;

        }
     
        public static bool CurrentlyRunning(string ipAddress,ushort portNo, string ProgramNo)
        {
            short mainProgram=0;
            string pgm2 = Convert.ToString(ReadRunningProgramNumber(ipAddress, portNo, out mainProgram));
            if ( ProgramNo.TrimStart(new char[]{'O'}).Equals(pgm2, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;//not running currently
        }

        public static string DownloadProgram(string ipAddress, ushort portNo, int programNo, out bool result, string folderPath , bool isProgramFolderSupports )
        {
            result = false;
            short ret = -20;
            int bufsize = 1280;
            int dataLength = 0;
            ushort focasLibHandle = 0;
            string programDownloaded = string.Empty;
            ret = FocasLib.cnc_allclibhndl3(ipAddress, portNo, 10, out focasLibHandle);
            if (ret != 0)
            {
                Logger.WriteErrorLog("cnc_allclibhndl3() failed. return value is = " + ret + "Not able to connect to CNC machine. Please check the network connection and try again");
                return string.Empty;
            }
            int count = 0;
            ret = short.MinValue;

            while (ret != 0 && count < 10)
            {
                ret = FocasLib.cnc_upstart3(focasLibHandle, 0, programNo, programNo);
                count++;
                if (ret == -1) Thread.Sleep(400);
            }
            if (ret == -1)
            {
                Logger.WriteDebugLog("CNC is busy. Please try after some time.");
                FocasLib.cnc_freelibhndl(focasLibHandle);
                return string.Empty;
            }

            if (ret != 0)
            {
                Logger.WriteErrorLog("cnc_upstart3() failed. return value is = " + ret);
                FocasLib.cnc_freelibhndl(focasLibHandle);
                return string.Empty;
            }
            StringBuilder programStr = new StringBuilder(bufsize * 10);
            bool endFound = false;
            do
            {
                char[] buf = new char[bufsize + 1];
                dataLength = bufsize;
                ret = FocasLib.cnc_upload3(focasLibHandle, ref dataLength, buf);

                if (ret == 10) // if buffer is empty retry
                {
                    Thread.Sleep(400);
                    continue;

                }
                if (ret == -2) //if buffer is in reset mode Write protected on CNC side
                {
                    Logger.WriteErrorLog("Reset or stop request. The data to read is nothing. cnc_upload3() failed. return value is = " + ret);
                    break;
                }
                if (ret == 7) //if buffer is in reset mode Write protected on CNC side
                {
                    Logger.WriteErrorLog("Write protected on CNC side.. cnc_upload3() failed. return value is = " + ret);
                    break;
                }
                if (ret != 0)
                {
                    Logger.WriteErrorLog("cnc_upload3() failed. return value is = " + ret);
                    break;
                }

                char[] tempBuf = new char[dataLength];
                Array.Copy(buf, tempBuf, dataLength);
                programStr.Append(tempBuf);
                if (buf[dataLength - 1] == '%')
                {
                    result = endFound = true;
                    break;
                }
                Thread.Sleep(600);

            } while (endFound == false);
            if (ret == 7)
            {
                FocasLib.cnc_freelibhndl(focasLibHandle);
                return string.Empty;
            }

            ret = FocasLib.cnc_upend3(focasLibHandle);
            if (ret != 0)
            {
                Logger.WriteErrorLog("cnc_upend3() failed. return value is = " + ret);
                return string.Empty;
            }
            if (string.IsNullOrEmpty(Convert.ToString(programStr)))
            {
                Logger.WriteDebugLog("program is empty.");
                return string.Empty;
            }
            programStr.Replace("\r", "").Replace("\n", "\r\n");
            result = true;
            FocasLib.cnc_freelibhndl(focasLibHandle);
            return programStr.ToString();            
        }
         public static string SafeFileName(string name)
        {
            StringBuilder str = new StringBuilder(name);
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                str = str.Replace(c, '_');
            }
            return str.ToString();
        }

        public static string SafePathName(string name)
        {
            StringBuilder str = new StringBuilder(name);

            foreach (char c in System.IO.Path.GetInvalidPathChars())
            {
                str = str.Replace(c, '_');
            }
            return str.ToString();
        }

        public static List<string> GetCNCFolders(ushort focasLibHandle, string folderName)
        {
            List<string> folders = new List<string>();
            FocasLibBase.ODBPDFNFIL NoOfSubFolder = new FocasLibBase.ODBPDFNFIL();
            short ret = FocasLib.cnc_rdpdf_subdirn(focasLibHandle, folderName, NoOfSubFolder);

            short num_prog = 1;
            if (NoOfSubFolder.dir_num > 0)
            {
                for (short i = 0; i < NoOfSubFolder.dir_num; i++)
                {
                    //cnc_rdpdf_alldir : Reads the file / folder information under the specified folder.                       
                    FocasLibBase.IDBPDFADIR pdf_adir_in = new FocasLibBase.IDBPDFADIR();
                    pdf_adir_in.path = folderName;
                    pdf_adir_in.req_num = i;
                    pdf_adir_in.type = 0;
                    pdf_adir_in.size_kind = 1;

                    num_prog = 1;
                    FocasLibBase.ODBPDFADIR1 pdf_adir_out1 = new FocasLibBase.ODBPDFADIR1();
                    ret = FocasLib.cnc_rdpdf_alldir(focasLibHandle, ref num_prog, pdf_adir_in, pdf_adir_out1);
                    if (pdf_adir_out1.info1.data_kind == 0)
                    {                        
                        folders.Add(pdf_adir_out1.info1.d_f);
                    }
                }
            }
            return folders;
        }

        public static ushort ConnectToCNC(string ipAddress, ushort portNo)
        {            
            short ret = -20;           
            ushort focasLibHandle = 0;          
            ret = FocasLib.cnc_allclibhndl3(ipAddress, portNo, 10, out focasLibHandle);
            if (ret != 0)
            {
                Logger.WriteErrorLog("cnc_allclibhndl3() failed. return value is = " + ret + "Not able to connect to CNC machine. Please check the network connection and try again");
                return ushort.MinValue;
            }
            return focasLibHandle;
        }

    }
}

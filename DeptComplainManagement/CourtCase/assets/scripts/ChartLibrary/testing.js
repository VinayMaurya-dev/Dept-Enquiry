


//function saveAllObtainMarks1() {
//  
//        jsonChildData = localStorage.getItem("ChildData");
//        var storedData = localStorage.getItem("PersonalDetails");
//        var womenProfile = JSON.parse(storedData);
//        if (womenProfile && womenProfile.length > 0) {
//            var personalData = womenProfile[0];
//            var MOrderBy = personalData.MOrderBy;
//            var MOrderdate = personalData.MOrderdate;
//            var WomanName = personalData.WomanName;
//            var WomenShelterHomes = personalData.WomenShelterHomes;
//            var TxtDoyouknowDateofBirth = personalData.TxtDoyouknowDateofBirth;
//            var TxtYearAgedeclared = personalData.TxtYearAgedeclared;
//            var Month = personalData.Month;
//            var TxtDay = personalData.TxtDay;
//            var MDOB = personalData.MDOB;
//            var MAge = personalData.MAge;
//            var FHName = personalData.FHName;
//            var MName = personalData.MName;
//            var MContactNo = personalData.MContactNo;
//            var PPhoneNo = personalData.PPhoneNo;
//            var TxtAadharisexistingOrnot = personalData.TxtAadharisexistingOrnot;
//            var MRegilion = personalData.MRegilion;
//            var MCategory = personalData.MCategory;
//            var MState = personalData.MState;
//            var MDistrict = personalData.MDistrict;
//            var MDistrict1 = personalData.MDistrict1;
//            var MUrban = personalData.MUrban;
//            var MBlock = personalData.MBlock;
//            var MGram = personalData.MGram;
//            var MWord = personalData.MWord;

//            var MMunicipal = personalData.MMunicipal;
//            var PoliceStation = personalData.PoliceStation;
//            var MAddress = personalData.MAddress;
//            var MAdharNo = personalData.MAdharNo;
//            var TxtMentionIfthereisAnyLifeThreateningdisease = personalData.TxtMentionIfthereisAnyLifeThreateningdisease;
//            var WomenProfile = personalData.WomenImgProfile;
//        }
//        //Child Details
//        var storedChildData = localStorage.getItem("WomenStatus");
//        var womenStatus = JSON.parse(storedChildData);
//        if (womenStatus && womenStatus.length > 0) {
//            var WomenStatus = womenStatus[0];
//            var MStatus = womenStatus.MStatus;
//            var MArrivalDate = womenStatus.MArrivalDate;
//            var MLeaveReason = womenStatus.MLeaveReason;
//            var WomenAccom = womenStatus.WomenAccom;
//            var NumberOfChild = womenStatus.NumberOfChild;
//        }
//        //Women Educated
//        var storedWomenEducated = localStorage.getItem("WomenEdcated");
//        var JWomenEdcated = JSON.parse(storedWomenEducated);
//        if (JWomenEdcated && JWomenEdcated.length > 0) {
//            var WomenEdcatation = JWomenEdcated[0];
//            var WomenIsEducated = JWomenEdcated.WomenIsEducated;
//            var IsInmateGettingAnyTypeOfTraining = JWomenEdcated.IsInmateGettingAnyTypeOfTraining;
//            var MTraining = JWomenEdcated.MTraining;
//            var Tradofvoctraiining = JWomenEdcated.Tradofvoctraiining;
//            var Begincourse = JWomenEdcated.Begincourse;
//            var Endofcourse = JWomenEdcated.Endofcourse;
//            var MQualification = JWomenEdcated.MQualification;
//            var IstheInmatePursuingFurtherStudies = JWomenEdcated.IstheInmatePursuingFurtherStudies;
//        }
//        //Rehaibilited Women
//         var OtherReason = $('#ContentPlaceHolder1_txtother').val();
//         var Catspeciallyabledwomen = $('#ContentPlaceHolder1_ddlcatspeciallyabledwomen').val();
//         var CrimeNo = $('#ContentPlaceHolder1_txtcrimeno').val();
//         var Dateofbegincase = $('#ContentPlaceHolder1_txtdateofbegincase').val();
//         var Tradofskilltraiining = $('#ContentPlaceHolder1_txttradofskilltraiining').val();
//         var Tradofproftraiining = $('#ContentPlaceHolder1_txttradofproftraiining').val();
//         var Durationcourse = $('#ContentPlaceHolder1_txtdurationcourse').val();
//         var Jobplacement = $('#ContentPlaceHolder1_ddljobplacement1').val();
//         var CompanyName = $('#ContentPlaceHolder1_txtcompanyname').val();
//         var CurrentSalaryPermonth = $('#ContentPlaceHolder1_txtcurrentsalarypermonth').val();
//         var AnyEquipment = $('#ContentPlaceHolder1_txtanyequipment').val();
//         var AnyFinancialAid = $('#ContentPlaceHolder1_txtanyfinancialaid').val();
//         var Dateleavingshelterhome = $('#ContentPlaceHolder1_txtDataOfLeavingShelterHome').val();
//         var releaseorder = $('#ContentPlaceHolder1_ddlReleaseOrderBy').val();
//         var RealseOrderdate = $('#ContentPlaceHolder1_TxtReleaseOrderDate').val();
//         var divreasonforleavingshelterhome =  $('#ContentPlaceHolder1_ddlreasonforleavingshelterhome').val();

//         // Add new Columns 
//         var TxtCourtName = $('#ContentPlaceHolder1_courtname').val();
//         var TxtOrderNo = $('#ContentPlaceHolder1_orderno').val();
//         var TxtDetailsofCaseField = $('#ContentPlaceHolder1_dtlscsefld').val();
//         var TxtDateWhenDeclaredAgeoftheWomen = $('#ContentPlaceHolder1_txtdeclaredage').val();
//         var TxtInmateremark = $('#ContentPlaceHolder1_txxtother').val();
//         var TxtPermissionBy = $('#ContentPlaceHolder1_txtpermission').val();
//         var TxtInmateName = $('#ContentPlaceHolder1_tetxinmatenm').val();
//         var TxtFirmName = $('#ContentPlaceHolder1_txtfirmname').val();
//         var TxtJobDesignation = $('#ContentPlaceHolder1_txtdesig').val();
//         var TxtDateofJoining = $('#ContentPlaceHolder1_txtjoiningdt').val();
//         var TxtCurrentSalary = $('#ContentPlaceHolder1_txtcrrntslry').val();
//         var Txtstate = $('#ContentPlaceHolder1_txtstate').val();
//         var TxtTransferotherStateReason = $('#ContentPlaceHolder1_txtreason').val();
//         var TxtTransferAfterCareHomes = $('#ContentPlaceHolder1_txtaftrcrhm').val();

//         var TxtFamilyContact = $('#ContentPlaceHolder1_txtPhoneNumber').val();
//         var InwhichClassInmateIsStudying = $('#ContentPlaceHolder1_txtinmatewhichclass').val();
//         var ReleaseStatus = $('#ContentPlaceHolder1_txtstatusrelease').val();
//         var NamePersonInmateAfterRelease = $('#ContentPlaceHolder1_txtnamepersonwithgone').val();
//         var TxtContactNumber = $('#ContentPlaceHolder1_txtinmategonecontact').val();
//         var txtInmateGaurdianAddress = $('#ContentPlaceHolder1_txtinmatepersonaddress').val();
//         var InmateWorking = $('#ContentPlaceHolder1_txtreasonleaving').val();
//         var AddressOftheCompany = $('#ContentPlaceHolder1_txtcompanmyaddress').val();
//         var InmateWorkingDistrict = $('#ContentPlaceHolder1_txtdistrictcompany').val();
//    var TradeOfBusiness = $('#ContentPlaceHolder1_txttradeofbusiness').val();

//        const arrivaldate = new Date(ConvertFormatmmddyy(MArrivalDate));
//         const leavingdate = new Date(ConvertFormatmmddyy(Dateleavingshelterhome));


//    var jsondata = jsonChildData;
//    var Regdata = "{WomenShelterHomes: '" + WomenShelterHomes + "',WomanName: '" + WomanName + "',MDOB: '" + ConvertFormatmmddyy(MDOB) + "',MAge: '"
//        + MAge + "',FHName: '" + FHName + "',MName: '" + MName + "',MState: '" + MState + "',MDistrict: '" + MDistrict + "',MDistrict1:'" + MDistrict1 +
//        "',MContactNo: '" + MContactNo + "',MAdharNo: '" + MAdharNo + "',MRegilion: '" + MRegilion + "',MCategory: '" + MCategory + "',MUrban: '" + MUrban +
//        "',MBlock:'" + MBlock + "',MGram:'" + MGram + "',MWord:'" + MWord + "',MMunicipal:'" + MMunicipal + "',PoliceStation: '" + PoliceStation + "',PPhoneNo: '"
//        + PPhoneNo + "',MOrderBy: '" + MOrderBy + "',MOrderdate: '" + ConvertFormatmmddyy(MOrderdate) + "',WomenAccom: '" + WomenAccom + "',MAddress: '" + MAddress +
//        "',MStatus: '" + MStatus + "',MArrivalDate: '" + ConvertFormatmmddyy(MArrivalDate) + "',MLeaveReason: '" + MLeaveReason + "',MQualification: '" +
//        MQualification + "',MTraining: '" + MTraining + "',NumberOfChild: '" + NumberOfChild + "' ,OtherReason: '" + OtherReason + "',Catspeciallyabledwomen: '" +
//        Catspeciallyabledwomen + "',CrimeNo: '" + CrimeNo + "',Dateofbegincase: '" + ConvertFormatmmddyy(Dateofbegincase) + "'     ,Tradofskilltraiining: '" +
//        Tradofskilltraiining + "',Tradofproftraiining: '" + Tradofproftraiining + "',Tradofvoctraiining: '" + Tradofvoctraiining + "',Begincourse: '" +
//        ConvertFormatmmddyy(Begincourse) + "'  ,Endofcourse: '" + ConvertFormatmmddyy(Endofcourse) + "',Durationcourse: '" + Durationcourse + "',Jobplacement: '" +
//        Jobplacement + "',CompanyName: '" + CompanyName + "'  ,CurrentSalaryPermonth: '" + CurrentSalaryPermonth + "',AnyEquipment: '" + AnyEquipment
//        + "',AnyFinancialAid: '" + AnyFinancialAid + "',Dateleavingshelterhome: '" + ConvertFormatmmddyy(Dateleavingshelterhome) +

//        "',divreasonforleavingshelterhome: '" + divreasonforleavingshelterhome + "',JsonData: '" + jsondata + "',releaseorder: '" + releaseorder +
//        "',RealseOrderdate: '" + ConvertFormatmmddyy(RealseOrderdate) + "',TxtCourtName:'" + TxtCourtName + "',TxtOrderNo:'" + TxtOrderNo + "',TxtDetailsofCaseField:'"
//        + TxtDetailsofCaseField + "',TxtDoyouknowDateofBirth:'" + TxtDoyouknowDateofBirth + "',TxtYearAgedeclared:'" + TxtYearAgedeclared + "',Month:'" + Month +
//        "',TxtDay:'" + TxtDay + "',TxtAadharisexistingOrnot:'" + TxtAadharisexistingOrnot + "',TxtDateWhenDeclaredAgeoftheWomen:'" + ConvertFormatmmddyy(TxtDateWhenDeclaredAgeoftheWomen) +
//        "',TxtMentionIfthereisAnyLifeThreateningdisease:'" + TxtMentionIfthereisAnyLifeThreateningdisease + "',TxtInmateremark:'" + TxtInmateremark +
//        "',TxtPermissionBy:'" + TxtPermissionBy + "',TxtInmateName:'" + TxtInmateName + "',TxtFirmName:'" + TxtFirmName + "',TxtJobDesignation:'" + TxtJobDesignation +
//        "',TxtDateofJoining:'" + ConvertFormatmmddyy(TxtDateofJoining) + "',TxtCurrentSalary:'" + TxtCurrentSalary + "',Txtstate:'" + Txtstate +
//        "',TxtTransferotherStateReason:'" + TxtTransferotherStateReason +
//        "',TxtTransferAfterCareHomes:'" + TxtTransferAfterCareHomes +
//        "',TxtFamilyContact:'" + TxtFamilyContact + "',WomenIsEducated:'" + WomenIsEducated + "',IstheInmatePursuingFurtherStudies:'" + IstheInmatePursuingFurtherStudies +
//        "',InwhichClassInmateIsStudying:'" + InwhichClassInmateIsStudying + "',IsInmateGettingAnyTypeOfTraining:'" + IsInmateGettingAnyTypeOfTraining +
//        "',ReleaseStatus:'" + ReleaseStatus + "',NamePersonInmateAfterRelease:'" + NamePersonInmateAfterRelease + "',TxtContactNumber:'" + TxtContactNumber +
//        "',txtInmateGaurdianAddress:'" + txtInmateGaurdianAddress + "',InmateWorking:'" + InmateWorking + "',AddressOftheCompany:'" + AddressOftheCompany +
//        "',InmateWorkingDistrict:'" + InmateWorkingDistrict + "',TradeOfBusiness:'" + TradeOfBusiness +
//        "',WomenProfile:'" + WomenProfile + "'}";
//         var result;
//       
//         $.ajax({    
//             type: "POST",
//             async: false,
//             contentType: "application/json; charset=utf-8",
//             url: "Mahila_Form1.aspx/PostRegData",
//             data: Regdata ,
//             dataType: "json",
//             beforeSend: function () {
//             },
//             success: function (data) {         
//                 result = data.d;

//                 debugger
//                 //swal({
//                 //    title: "Success!",
//                 //    text: result,
//                 //    icon: "success",
//                 //    button: "OK",
//                 //    timer: 25000
//                 //});

//                 swal({
//                     title: "Success",
//                     text: result,
//                     type: "success"
//                 },
//                     function () {
//                         location.reload();
//                     }
//                 );

//                // alert(result);
//                 Clear();
//                 localStorage.clear();
//                 localStorage.removeItem("ChildData");
//                 localStorage.removeItem("PersonalDetails");
//                 localStorage.removeItem("WomenStatus");
//                 localStorage.removeItem("WomenEdcated");
//               //  location.reload();
//             },
//             error: function (request, status, error) {
//                 console.log(request.responseText);
//             },
//             complete: function ()
//             {
//             }
//         });
//         return result === undefined ? "" : result;
//}


// Function to convert base64 to Blob
//function dataURItoBlob(dataURI) {
//    var byteString = atob(dataURI.split(",")[1]);
//    var mimeString = dataURI.split(",")[0].split(":")[1].split(";")[0];
//    var ab = new ArrayBuffer(byteString.length);
//    var ia = new Uint8Array(ab);
//    for (var i = 0; i < byteString.length; i++) {
//        ia[i] = byteString.charCodeAt(i);
//    }
//    return new Blob([ab], { type: mimeString });
//}
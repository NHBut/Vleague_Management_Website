﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Vleague_Management_Website.Models;

namespace Vleague_Management_Website.Controllers
{
	public class BangXepHangController : Controller
	{
        QlbongDaContext db = new QlbongDaContext();
        public IActionResult Index()
		{
            List<Caulacbo> lstCauLacBo = db.Caulacbos.ToList();
            List<BangXepHang> lstBangXepHang = new List<BangXepHang>();
			foreach(var item in lstCauLacBo)
			{
                var lstTranDau = (from a in db.Trandaus
                                  where
                                    a.Clbnha == item.CauLacBoId &&
                                    a.TrangThai == true
                                  select a
                                  ).ToList();
                var soTranThang = 0;
				var soTranThua = 0;
				var soTranHoa = 0;
				var diem = 0;

                foreach (var item1 in lstTranDau)
				{
					string[] ketqua = item1.KetQua.Split("-");
                    var banThangDoiNha = int.Parse(ketqua[0]);
                    var banThangDoiKhach = int.Parse(ketqua[1]);
					if(banThangDoiNha > banThangDoiKhach)
					{
						soTranThang += 1;
                    }
					if(banThangDoiNha < banThangDoiKhach)
					{
						soTranThua += 1;
					}
					if(banThangDoiNha == banThangDoiKhach)
					{
						soTranHoa += 1;
					}
                }
				diem = soTranThang * 3 + soTranHoa;
				lstBangXepHang.Add(new global::BangXepHang
				{
					TenClb = item.TenClb,
					SoTranThang = soTranThang,
					SoTranHoa = soTranHoa,
					SoTranThua = soTranThua,
					Diem = diem,
				});

            }
			var lstBangXepHangSort = lstBangXepHang.OrderByDescending(x => x.Diem).ToList();
            return View(lstBangXepHangSort);
		}

	}
}

public class BangXepHang
{
	public String TenClb;
	public int SoTranThang;
	public int SoTranThua;
	public int SoTranHoa;
	public int Diem;

}
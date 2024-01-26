using System.ComponentModel.DataAnnotations;

namespace Opas.Core.EnquiryService.Models;

public enum EnquiryStatus
{
	Unknown = 0,
	New = 1,
	[Display(Name = "Replied to")]
	Replied = 2,
	[Display(Name = "Had meeting")]
	HadMeeting = 3,
	[Display(Name = "Became member")]
	BecameMember = 4,
	[Display(Name = "Not interested")]
	NotInterested = 5
}
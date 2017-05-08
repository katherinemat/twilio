using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Twilio.Models;

namespace Twilio.Migrations
{
    [DbContext(typeof(TwilioContext))]
    [Migration("20170508214149_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Twilio.Models.Contact", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("ContactId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Twilio.Models.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<int?>("ContactId");

                    b.Property<string>("From");

                    b.Property<string>("Status");

                    b.Property<string>("To");

                    b.HasKey("MessageId");

                    b.HasIndex("ContactId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Twilio.Models.Message", b =>
                {
                    b.HasOne("Twilio.Models.Contact", "Contact")
                        .WithMany("Messages")
                        .HasForeignKey("ContactId");
                });
        }
    }
}

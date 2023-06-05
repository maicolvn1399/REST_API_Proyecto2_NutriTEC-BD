using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace REST_API_NutriTEC.Models;

public partial class Proyecto2Context : DbContext
{
    public Proyecto2Context()
    {
    }

    public Proyecto2Context(DbContextOptions<Proyecto2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Billing> Billings { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Clientxplan> Clientxplans { get; set; }

    public virtual DbSet<ConsumptionRecord> ConsumptionRecords { get; set; }

    public virtual DbSet<Dish> Dishes { get; set; }

    public virtual DbSet<Dishxplan> Dishxplans { get; set; }

    public virtual DbSet<Measurement> Measurements { get; set; }

    public virtual DbSet<Nutritionist> Nutritionists { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Productxdish> Productxdishes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Vitamin> Vitamins { get; set; }

    public virtual DbSet<Vitaminxdish> Vitaminxdishes { get; set; }

    public virtual DbSet<LoginClient> LoginClients { get; set; }

    public virtual DbSet<LoginNutritionist> LoginNutritionists { get; set; }

    public virtual DbSet<AddNutritionist> AddNutritionists { get; set; }

    public virtual DbSet<LoginAdmin> LoginAdmins { get; set; }

    public virtual DbSet<Calculate_billing> Calculate_billings { get; set; }

    public virtual DbSet<GetUnapprovedProduct> GetUnapprovedProducts { get; set; }

    public virtual DbSet<Approve_product> Approve_products { get; set; }

    public virtual DbSet<Delete_product> Delete_products { get; set; }

    public virtual DbSet<Delete_plan> Delete_plans { get; set; }

    public virtual DbSet<searchclient> searchclients { get; set; }

    public virtual DbSet<Associateclient> Associateclients { get; set; }

    public virtual DbSet<Create_product> Create_products { get; set; }

    public virtual DbSet<Add_Vitamin_Product> Add_Vitamin_Products { get; set; }

    public virtual DbSet<Get_daily_consumption> Get_daily_consumptions { get; set; }

    public virtual DbSet<Get_nutritionist_clients> Get_nutritionist_clientss { get; set; }

    public virtual DbSet<Get_nutritionist_plans> Get_nutritionist_planss { get; set; }

    public virtual DbSet<Assign_plan> Assign_plans { get; set; }

    public virtual DbSet<Create_plan> Create_plans { get; set; }

    public virtual DbSet<Add_dish_to_plan> Add_dish_to_plans { get; set; }

    public virtual DbSet<Search_product> Search_products { get; set; }

    public virtual DbSet<Createrecipe> Createrecipes { get; set; }

    public virtual DbSet<Add_product_to_recipe> Add_product_to_recipes { get; set; }

    public virtual DbSet<Update_recipe_values> Update_recipe_valuess { get; set; }

    public virtual DbSet<AddNewClient> AddNewClients { get; set; }

    public virtual DbSet<AddMeasurement> AddMeasurements { get; set; }

    public virtual DbSet<AddDailyIntake> AddDailyIntakes { get; set; }

    public virtual DbSet<GenerateReport> GenerateReports { get; set; }

    public virtual DbSet<ClientPlan> ClientPlans { get; set; }

    public virtual DbSet<PatientFollowUpJSON> PatientFollowUpJSONs { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=serverproyecto2.postgres.database.azure.com;Database=proyecto2;Username=postgresqladmin;Password=Naheem.1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Billing>(entity =>
        {
            entity.HasKey(e => e.BillingId).HasName("billing_pkey");

            entity.ToTable("billing");

            entity.Property(e => e.BillingId).HasColumnName("billing_id");
            entity.Property(e => e.BillingType)
                .HasMaxLength(100)
                .HasColumnName("billing_type");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("client_pkey");

            entity.ToTable("client");

            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .HasColumnName("email");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.CalorieGoal).HasColumnName("calorie_goal");
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .HasColumnName("client_name");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Lastname1)
                .HasMaxLength(100)
                .HasColumnName("lastname1");
            entity.Property(e => e.Lastname2)
                .HasMaxLength(100)
                .HasColumnName("lastname2");
            entity.Property(e => e.NutritionistId)
                .HasMaxLength(100)
                .HasColumnName("nutritionist_id");
            entity.Property(e => e.Pass)
                .HasMaxLength(100)
                .HasColumnName("pass");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Nutritionist).WithMany(p => p.Clients)
                .HasForeignKey(d => d.NutritionistId)
                .HasConstraintName("client_fk");
        });

        modelBuilder.Entity<Clientxplan>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("clientxplan");

            entity.Property(e => e.ClientId)
                .HasMaxLength(250)
                .HasColumnName("client_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.PlanName)
                .HasMaxLength(100)
                .HasColumnName("plan_name");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.Client).WithMany()
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("clientxplan_fk1");

            entity.HasOne(d => d.PlanNameNavigation).WithMany()
                .HasForeignKey(d => d.PlanName)
                .HasConstraintName("clientxplan_fk2");
        });

        modelBuilder.Entity<ConsumptionRecord>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("consumption_record");

            entity.Property(e => e.ClientId)
                .HasMaxLength(100)
                .HasColumnName("client_id");
            entity.Property(e => e.DateC).HasColumnName("date_c");
            entity.Property(e => e.DishName)
                .HasMaxLength(100)
                .HasColumnName("dish_name");
            entity.Property(e => e.FoodTime)
                .HasMaxLength(100)
                .HasColumnName("food_time");
            entity.Property(e => e.Serving).HasColumnName("serving");

            entity.HasOne(d => d.Client).WithMany()
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("consumption_record_fk");

            entity.HasOne(d => d.DishNameNavigation).WithMany()
                .HasPrincipalKey(p => p.DishName)
                .HasForeignKey(d => d.DishName)
                .HasConstraintName("consumption_record_fk1");
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.Barcode).HasName("dish_pkey");

            entity.ToTable("dish");

            entity.HasIndex(e => e.DishName, "dish_dish_name_key").IsUnique();

            entity.Property(e => e.Barcode).HasColumnName("barcode");
            entity.Property(e => e.Active)
                .HasColumnType("bit(1)")
                .HasColumnName("active");
            entity.Property(e => e.Calcium).HasColumnName("calcium");
            entity.Property(e => e.Calories).HasColumnName("calories");
            entity.Property(e => e.Carbs).HasColumnName("carbs");
            entity.Property(e => e.DishName)
                .HasMaxLength(100)
                .HasColumnName("dish_name");
            entity.Property(e => e.DishSize).HasColumnName("dish_size");
            entity.Property(e => e.Fat).HasColumnName("fat");
            entity.Property(e => e.Iron).HasColumnName("iron");
            entity.Property(e => e.Protein).HasColumnName("protein");
            entity.Property(e => e.Sodium).HasColumnName("sodium");
        });

        modelBuilder.Entity<Dishxplan>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dishxplan");

            entity.Property(e => e.DishId).HasColumnName("dish_id");
            entity.Property(e => e.FoodTime)
                .HasMaxLength(100)
                .HasColumnName("food_time");
            entity.Property(e => e.PlanId)
                .HasMaxLength(100)
                .HasColumnName("plan_id");

            entity.HasOne(d => d.Dish).WithMany()
                .HasForeignKey(d => d.DishId)
                .HasConstraintName("dishxplan_fk2");

            entity.HasOne(d => d.Plan).WithMany()
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("dishxplan_fk1");
        });

        modelBuilder.Entity<Measurement>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("measurement");

            entity.Property(e => e.ClientId)
                .HasMaxLength(100)
                .HasColumnName("client_id");
            entity.Property(e => e.DateM).HasColumnName("date_m");
            entity.Property(e => e.FatPercentage)
                .HasMaxLength(5)
                .HasColumnName("fat_percentage");
            entity.Property(e => e.Hip).HasColumnName("hip");
            entity.Property(e => e.MusclePercentage)
                .HasMaxLength(5)
                .HasColumnName("muscle_percentage");
            entity.Property(e => e.Neck).HasColumnName("neck");
            entity.Property(e => e.Waist).HasColumnName("waist");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Client).WithMany()
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("measurement_fk");
        });

        modelBuilder.Entity<Nutritionist>(entity =>
        {
            entity.HasKey(e => e.NutriCode).HasName("nutritionist_pkey");

            entity.ToTable("nutritionist");

            entity.Property(e => e.NutriCode)
                .HasMaxLength(100)
                .HasColumnName("nutri_code");
            entity.Property(e => e.Address)
                .HasMaxLength(1000)
                .HasColumnName("address");
            entity.Property(e => e.BillingId).HasColumnName("billing_id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.CreditCard)
                .HasMaxLength(100)
                .HasColumnName("credit_card");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Lastname1)
                .HasMaxLength(100)
                .HasColumnName("lastname1");
            entity.Property(e => e.Lastname2)
                .HasMaxLength(100)
                .HasColumnName("lastname2");
            entity.Property(e => e.NutritionistId)
                .HasMaxLength(100)
                .HasColumnName("nutritionist_id");
            entity.Property(e => e.NutritionistName)
                .HasMaxLength(100)
                .HasColumnName("nutritionist_name");
            entity.Property(e => e.Pass)
                .HasMaxLength(100)
                .HasColumnName("pass");
            entity.Property(e => e.Photo)
                .HasMaxLength(10000)
                .HasColumnName("photo");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Billing).WithMany(p => p.Nutritionists)
                .HasForeignKey(d => d.BillingId)
                .HasConstraintName("nutritionist_fk");

            entity.HasOne(d => d.Role).WithMany(p => p.Nutritionists)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("nutritionist_fk1");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.PlanName).HasName("pk_plan");

            entity.ToTable("plan");

            entity.Property(e => e.PlanName)
                .HasMaxLength(1000)
                .HasColumnName("plan_name");
            entity.Property(e => e.NutritionistId)
                .HasMaxLength(100)
                .HasColumnName("nutritionist_id");
            entity.Property(e => e.TotalCalories).HasColumnName("total_calories");

            entity.HasOne(d => d.Nutritionist).WithMany(p => p.Plans)
                .HasForeignKey(d => d.NutritionistId)
                .HasConstraintName("plan_fk");
        });

        modelBuilder.Entity<Productxdish>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("productxdish");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductServing).HasColumnName("product_serving");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(100)
                .HasColumnName("recipe_name");

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("productxdish_fk1");

            entity.HasOne(d => d.RecipeNameNavigation).WithMany()
                .HasPrincipalKey(p => p.DishName)
                .HasForeignKey(d => d.RecipeName)
                .HasConstraintName("productxdish_fk");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Vitamin>(entity =>
        {
            entity.HasKey(e => e.VitaminId).HasName("pk_vitamin");

            entity.ToTable("vitamin");

            entity.Property(e => e.VitaminId)
                .HasMaxLength(3)
                .HasColumnName("vitamin_id");
            entity.Property(e => e.VitaminName)
                .HasMaxLength(100)
                .HasColumnName("vitamin_name");
        });

        modelBuilder.Entity<Vitaminxdish>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("vitaminxdish");

            entity.Property(e => e.DishId).HasColumnName("dish_id");
            entity.Property(e => e.Vitamin)
                .HasMaxLength(2)
                .HasColumnName("vitamin");

            entity.HasOne(d => d.Dish).WithMany()
                .HasForeignKey(d => d.DishId)
                .HasConstraintName("vitaminxdish_fk");

            entity.HasOne(d => d.VitaminNavigation).WithMany()
                .HasForeignKey(d => d.Vitamin)
                .HasConstraintName("vitaminxdish_fk1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);






}




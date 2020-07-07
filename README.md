# Vanlo C# .NET SDK

Vanlo offers a simple shipping API for rating and buying shipping labels.

## Documentation

Up-to-date documentation at: https://api-docs.vanlo.com

### Configuration

During the initialization of your application add the following to configure Vanlo.  The second line demonstrates how to use the SDK in "test" mode.

```cs
using Vanlo;

ClientManager.SetCurrent("YOUR_PROD_API_KEY");
// ClientManager.SetCurrent("YOUR_TEST_API_KEY", https://test.vanlo.com/api/v1);
```

### Address Verification

An `Address` can be verified.

```cs
using Vanlo;

Address address = new Address() {
    name = "John Doe",
    street1 = "722 Montgomery St, Ste. 305",
		street2 = "",
    city = "San Francisco",
    state = "CA",
    country = "US",
    zip = "94111",
    verify = new List<string>() { "delivery" }
};

address.Create();

if (address.verifications.delivery.success) {
    // successful verification
} else {
    // unsuccessful verification
}
```

### Rating

Rating is available through the `Shipment` object.

```cs
Address fromAddress = new Address() { zip = "10038-1219" };
Address toAddress = new Address() { zip = "94111" };

Parcel parcel = new Parcel() {
    width = 10.0,
    length = 9.0,
    height = 8.0,
    weight = 15.9,
		predefined_package = "Parcel"
};

Shipment shipment = new Shipment() {
    from_address = fromAddress,
    to_address = toAddress,
    parcel = parcel
};

shipment.Create();

foreach (Rate rate in shipment.rates) {
    // process rates
}
```

### Create and Buy Postage Label

You can create and buy a shipment in either one step or two to generate a postage label and tracking code:

```cs
Address fromAddress = new Address() { id = "adr_..." };
Address toAddress = new Address() {
    name = "John Dow",
    street1 = "722 Montgomery St, Ste. 305",
		street2 = "",
    city = "San Francisco",
    state = "CA",
    country = "US",
    zip = "94111"
};

Parcel parcel = new Parcel() {
    length = 12.0,
    width = 11.2,
    height = 9.5,
    weight = 15.9
};

CustomsItem item = new CustomsItem() {
    description = "Vanlo T-shirts",
    quantity = 2,
    value = 23.56,
    weight = 33,
    origin_country = "us",
    hs_tariff_number = "123456"
};
CustomsInfo info = new CustomsInfo() {
		customs_signer = "Warehouse Manager",
    customs_certify = "true",
    eel_pfc = "NOEEI 30.37(a)",
    customs_items = new List<CustomsItem>() { item }
};

Options options = new Options() {
    label_format = "zpl",
    postage_label_inline = true,
    delivery_confirmation = "NO_SIGNATURE",
		create_and_buy = true // create and buy in one step
};

Shipment shipment = new Shipment() {
    from_address = fromAddress,
    to_address = toAddress,
    parcel = parcel,
    customs_info = info,
    options = options,
		service = "Priority" // required to create and buy in one step
};

shipment.Create();
// shipment.Buy(shipment.LowestRate()) // if options.create_and_buy was false

// shipment.tracking_code;
// shipment.postage_label.label_url;
```

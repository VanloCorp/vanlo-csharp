# Vanlo .Net Client Library

Vanlo offers a simple shipping API. Please visit our support pages at https://support.vanlo.com for documentation and assistance.

## Documentation

Up-to-date documentation at: https://api-docs.vanlo.com

### Configuration

During the initialization of your application add the following to configure Vanlo.

```cs
using Vanlo;

ClientManager.SetCurrent("ApiKey");
```

### Address Verification

An `Address` can be verified.

```cs
using Vanlo;

Address address = new Address() {
    name = "John Dow",
    street1 = "722 Montgomery St, Ste. 305",
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
    weight = 15.9
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

### Postage Label Generation

Purchasing a shipment will generate a `PostageLabel`.

```cs
Address fromAddress = new Address() { id = "adr_..." };
Address toAddress = new Address() {
    name = "John Dow",
    street1 = "722 Montgomery St, Ste. 305",
    city = "San Francisco",
    state = "CA",
    country = "US",
    zip = "94111"
};

Parcel parcel = new Parcel() {
    width = 15.2,
    length = 18,
    height = 9.5,
    weight = 10
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
    customs_certify = true,
    eel_pfc = "NOEEI 30.37(a)",
    customs_items = new List<CustomsItem>() { item }
};

Options options = new Options() {
    label_format = "zpl",
    label_size = "4X6",
    label_date= new DateTime(2020, 4, 25, 8, 30, 52), //Make sure to set this date in future
    postage_label_inline = true,
    delivery_confirmation = "NO_SIGNATURE"
};

Shipment shipment = new Shipment() {
    from_address = fromAddress,
    to_address = toAddress,
    parcel = parcel,
    customs_info = info,
    options = options
};

shipment.Create();
shipment.Buy(shipment.LowestRate());

shipment.postage_label.url;
